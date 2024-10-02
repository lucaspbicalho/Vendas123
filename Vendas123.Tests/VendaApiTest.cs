using Bogus;
using Bogus.Extensions.Brazil;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using NSubstitute;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Vendas123.Api.Controllers;
using Vendas123.Domain.Entites;
using Vendas123.Domain.ViewModel;
using Vendas123.Infrastructure.Contexts;
using Vendas123.Infrastructure.Repositories;
using Vendas123.Services.Services;
using static System.Formats.Asn1.AsnWriter;

namespace Vendas123.Tests
{
    public class VendaApiTest
    {
        private WebApplicationFactory<Program> _factory;
        private ClienteViewModel _cliente { get; set; }
        private List<ProdutoViewModel> _listProdutos { get; set; }
        private Venda _venda { get; set; }
        private List<VendaCreateViewModel> _vendaCreate { get; set; }
        private List<VendaUpdateViewModel> _vendaUpdate { get; set; }

        public VendaApiTest()
        {
            //Arrange
            _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(webBuilder =>
            {
                webBuilder.ConfigureTestServices(services =>
                {
                    services.RemoveAll(typeof(DbContextOptions<VendasDbContext>));
                    services.AddDbContext<VendasDbContext>(opt =>
                    {
                        opt.UseInMemoryDatabase("VendasDb");
                    });

                });
            });

            _cliente = new Faker<ClienteViewModel>("pt_BR").StrictMode(true)
                     .RuleFor(p => p.Nome, f => f.Name.FullName())
                     .RuleFor(p => p.Cpf, f => f.Person.Cpf(true))
                     .RuleFor(p => p.Telefone, f => f.Person.Phone)
                     .RuleFor(p => p.Email, (f, p) => f.Person.Email.ToLower())
                     .Generate(1).FirstOrDefault();

            _listProdutos = new Faker<ProdutoViewModel>("pt_BR").StrictMode(true)
                       .RuleFor(p => p.CodProduto, f => f.Random.Number(int.MaxValue))
                       .RuleFor(p => p.Nome, f => f.Commerce.Product())
                       .RuleFor(p => p.ValorUnitario, f => f.Finance.Amount((decimal)0.01, 100))
                       .RuleFor(p => p.ValorTotal, f => 0)
                       .RuleFor(p => p.quantidade, f => f.Random.Number(1, 10))
                       .RuleFor(p => p.Desconto, f => 0)
                       .RuleFor(p => p.Status, f => f.Random.Number(1))
                       .Generate(new Random().Next(1, 10));

            _vendaCreate = new Faker<VendaCreateViewModel>("pt_BR").StrictMode(true)
                       .RuleFor(p => p.Filial, f => (int)f.PickRandom<Filial>())
                       .RuleFor(p => p.Produtos, f => _listProdutos)
                       .RuleFor(p => p.Cliente, f => _cliente)
                       .Generate(1);

            _venda = new Faker<Venda>("pt_BR").StrictMode(true)
                       .RuleFor(p => p.Id, Guid.Empty)
                       .RuleFor(p => p.CodVenda, 99)
                       .RuleFor(p => p.DataVenda, DateTime.Now)
                       .RuleFor(p => p.Valor, 0)
                       .RuleFor(p => p.Filial, f => f.PickRandom<Filial>())
                       .RuleFor(p => p.Produtos, f => _listProdutos.Select(x => (Produto)x).ToList())
                       .RuleFor(p => p.Cliente, f => (Cliente)_cliente)
                       .Generate(1).FirstOrDefault();

            _vendaUpdate = new Faker<VendaUpdateViewModel>("pt_BR").StrictMode(true)
                       .RuleFor(p => p.Valor, f => (int)f.PickRandom<Filial>())
                       .RuleFor(p => p.Filial, f => (int)f.PickRandom<Filial>())
                       .RuleFor(p => p.Cliente, f => _cliente)
                       .Generate(1);
        }


        [Fact]
        public void ValidVenda_GetAll()
        {
            //Arrange
            using (var scope = _factory.Services.CreateScope())
            {
                var scopService = scope.ServiceProvider;
                var dbContext = scopService.GetService<VendasDbContext>();

                dbContext.Database.EnsureCreated();
                dbContext.Vendas.Add(_venda);
                dbContext.SaveChanges();
            }
            var client = _factory.CreateClient();

            //Act
            var response = client.GetAsync("api/vendas").Result;
            var result = response.Content.ReadFromJsonAsync<List<VendaCreateViewModel>>();

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            //TO DO - mensagem rabbitMQ
        }

        [Fact]
        public void ValidVenda_Save()
        {
            //Arrange
            using (var scope = _factory.Services.CreateScope())
            {
                var scopService = scope.ServiceProvider;
                var dbContext = scopService.GetService<VendasDbContext>();

                dbContext.Database.EnsureCreated();
                dbContext.Vendas.Add(_venda);
                dbContext.SaveChanges();
            }
            var client = _factory.CreateClient();

            //Act
            var myContent = JsonConvert.SerializeObject(_vendaCreate.FirstOrDefault());
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = client.PostAsync("api/vendas", byteContent).Result;            

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);

            //TO DO - mensagem rabbitMQ

        }
        [Fact]
        public void IsInvalidVendaNull_Save()
        {
            //Arrange
            using (var scope = _factory.Services.CreateScope())
            {
                var scopService = scope.ServiceProvider;
                var dbContext = scopService.GetService<VendasDbContext>();

                dbContext.Database.EnsureCreated();
                dbContext.Vendas.Add(_venda);
                dbContext.SaveChanges();
                dbContext.Database.EnsureDeleted();
            }
            var client = _factory.CreateClient();

            //Act
            var myContent = JsonConvert.SerializeObject(null);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = client.PostAsync("api/vendas", byteContent).Result;

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);

            //TO DO - mensagem rabbitMQ
        }

    }
}