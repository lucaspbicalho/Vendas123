using Bogus;
using Bogus.Extensions.Brazil;
using Vendas123.Domain.Entites;
using Vendas123.Domain.ViewModel;

namespace Vendas123.Tests.Fixtures
{
    public static class DataFixture
    {
        public static List<ClienteViewModel> GetClientesViewModel(int count = 1)
        {
            return new Faker<ClienteViewModel>("pt_BR").StrictMode(true)
                     .RuleFor(p => p.Nome, f => f.Name.FullName())
                     .RuleFor(p => p.Cpf, f => f.Person.Cpf(true))
                     .RuleFor(p => p.Telefone, f => f.Person.Phone)
                     .RuleFor(p => p.Email, (f, p) => f.Person.Email.ToLower())
                     .Generate(count);
        }
        public static List<ProdutoViewModel> GetProdutosViewModel(int count = 1)
        {
            return new Faker<ProdutoViewModel>("pt_BR").StrictMode(true)
                       .RuleFor(p => p.CodProduto, f => f.Random.Number(int.MaxValue))
                       .RuleFor(p => p.Nome, f => f.Commerce.Product())
                       .RuleFor(p => p.ValorUnitario, f => f.Finance.Amount((decimal)0.01, 100))
                       .RuleFor(p => p.ValorTotal, f => 0)
                       .RuleFor(p => p.quantidade, f => f.Random.Number(1, 10))
                       .RuleFor(p => p.Desconto, f => 0)
                       .RuleFor(p => p.Status, f => f.Random.Number(1))
                       .Generate(count);
        }
        public static List<VendaCreateViewModel> GetVendasViewModel(int count = 1)
        {
            return new Faker<VendaCreateViewModel>("pt_BR").StrictMode(true)
                       .RuleFor(p => p.Filial, f => (int)f.PickRandom<Filial>())
                       .RuleFor(p => p.Produtos, f => GetProdutosViewModel(new Random().Next(1, 10)))
                       .RuleFor(p => p.Cliente, GetClientesViewModel(1).FirstOrDefault())
                       .Generate(count);
        }
        public static Venda GetVenda()
        {
            return new Faker<Venda>("pt_BR").StrictMode(true)
                       .RuleFor(p => p.Id, Guid.Empty)
                       .RuleFor(p => p.CodVenda, 99)
                       .RuleFor(p => p.DataVenda, DateTime.Now)
                       .RuleFor(p => p.Valor, 0)
                       .RuleFor(p => p.Filial, f => f.PickRandom<Filial>())
                       .RuleFor(p => p.Produtos, f => GetProdutosViewModel(new Random().Next(1, 10)).Select(x => (Produto)x).ToList())
                       .RuleFor(p => p.Cliente, f => (Cliente)GetClientesViewModel(1).FirstOrDefault())
                       .Generate(1).FirstOrDefault();
        }        
    }
}
