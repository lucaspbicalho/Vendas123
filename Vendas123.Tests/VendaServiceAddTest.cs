using Bogus;
using Bogus.Extensions.Brazil;
using FluentAssertions;
using NSubstitute;
using Vendas123.Domain.Entites;
using Vendas123.Domain.ViewModel;
using Vendas123.Infrastructure.Repositories;
using Vendas123.Services.Services;

namespace Vendas123.Tests
{
    public class VendaServiceAddTest
    {
        private ClienteViewModel _cliente { get; set; }
        private List<ProdutoViewModel> _listProdutos { get; set; }
        private List<Venda> _venda { get; set; }
        private List<VendaCreateViewModel> _vendaCreate { get; set; }
        private List<VendaUpdateViewModel> _vendaUpdate { get; set; }

        public VendaServiceAddTest()
        {
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
                       .Generate(1);

            _vendaUpdate = new Faker<VendaUpdateViewModel>("pt_BR").StrictMode(true)
                       .RuleFor(p => p.Valor, f => (int)f.PickRandom<Filial>())
                       .RuleFor(p => p.Filial, f => (int)f.PickRandom<Filial>())
                       .RuleFor(p => p.Cliente, f => _cliente)
                       .Generate(1);
        }

        [Fact]
        public void ValidVenda_Save()
        {
            var venda = _venda.FirstOrDefault();
            var vendaCreateViewModel = _vendaCreate.FirstOrDefault();
            
            var vendaRepository = Substitute.For<IVendaRepository>();
            vendaRepository.Save(venda);
            var vendaService = Substitute.For<IVendaService>();
            vendaService.Save(vendaCreateViewModel);

            _cliente.Should().NotBeNull();
            _listProdutos.Should().NotBeNull();
            _vendaCreate.Should().NotBeNull();

            vendaRepository.Received().Save(venda);
            vendaService.Received().Save(vendaCreateViewModel);
        }
        [Fact]
        public void IsInvalidVendaNull_Save()
        {
            var vendaRepository = Substitute.For<IVendaRepository>();
            vendaRepository.Save(Arg.Any<Venda>());
            var vendaService = Substitute.For<IVendaService>();
            vendaService.Save(Arg.Any<VendaCreateViewModel>());

            vendaRepository.DidNotReceive().Save(Arg.Any<Venda>());
            vendaService.DidNotReceive().Save(Arg.Any<VendaCreateViewModel>());
        }
        [Fact]
        public void ValidVenda_Update()
        {
            var venda = _venda.FirstOrDefault();
            var vendaUpdate = _vendaUpdate.FirstOrDefault();

            var vendaRepository = Substitute.For<IVendaRepository>();
            vendaRepository.Update(venda.CodVenda, venda);
            var vendaService = Substitute.For<IVendaService>();
            vendaService.Update(venda.CodVenda, vendaUpdate);

            vendaRepository.Received().Update(venda.CodVenda, venda);
            vendaService.Received().Update(venda.CodVenda, vendaUpdate);
        }
        [Fact]
        public void IsInvalidVenda_Update()
        {
            var vendaRepository = Substitute.For<IVendaRepository>();
            vendaRepository.Update(0, Arg.Any<Venda>());
            var vendaService = Substitute.For<IVendaService>();
            vendaService.Update(0, Arg.Any<VendaUpdateViewModel>());

            vendaRepository.DidNotReceive().Update(0, Arg.Any<Venda>());
            vendaService.DidNotReceive().Update(0, Arg.Any<VendaUpdateViewModel>());
        }
        [Fact]
        public void ValidVenda_Delete()
        {
            var venda = _venda.FirstOrDefault();
            var vendaUpdate = _vendaUpdate.FirstOrDefault();

            var vendaRepository = Substitute.For<IVendaRepository>();
            vendaRepository.Delete(venda.CodVenda);
            var vendaService = Substitute.For<IVendaService>();
            vendaService.Delete(venda.CodVenda);

            vendaRepository.Received().Delete(venda.CodVenda);
            vendaService.Received().Delete(venda.CodVenda);
        }
        [Fact]
        public void IsInvalidVenda_Delete()
        {
            var vendaRepository = Substitute.For<IVendaRepository>();
            var resultRepo = vendaRepository.Delete(0);
            var vendaService = Substitute.For<IVendaService>();
            var resultService = vendaService.Delete(0);
        }
    }
}