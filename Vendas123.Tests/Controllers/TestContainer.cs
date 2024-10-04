using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendas123.Domain.ViewModel;
using Vendas123.Tests.Fixtures;

namespace Vendas123.Tests.Controllers
{
    public class TestContainer
    {
        [Fact]
        public async Task ValidVenda_GetAll()
        {
            //Arrange
            var newVendas = DataFixture.GetVendasViewModel(new Random().Next(1, 10)).FirstOrDefault();

            //Act
            var myContent = JsonConvert.SerializeObject(newVendas);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response2 = _factory._client.PostAsync("api/vendas", byteContent).Result;

            //Act
            var response = _factory._client.GetAsync("api/vendas").Result;
            var result = response.Content.ReadFromJsonAsync<List<VendaCreateViewModel>>();

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            //TO DO - mensagem rabbitMQ
        }
        [Fact]
        public async Task ValidVenda_Save()
        {
            //Arrange
            var newVendas = DataFixture.GetVendasViewModel(new Random().Next(1, 10)).FirstOrDefault();

            //Act
            var myContent = JsonConvert.SerializeObject(newVendas);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = _factory._client.PostAsync("api/vendas", byteContent).Result;

            //to do get

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);


            //TO DO - mensagem rabbitMQ
        }
    }
}
