using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vendas123.Domain.Entites;

namespace Vendas123.Domain.ViewModel
{
    public class VendaCreateViewModel
    {
        [Range(0, 2, ErrorMessage = "O campo {0} precisa ser entre 0 e 2.")]
        [DefaultValue(1)]
        public int Filial { get; set; }
        public ClienteViewModel Cliente { get; set; }
        public List<ProdutoViewModel> Produtos { get; set; }

        public static implicit operator Venda(VendaCreateViewModel vendaVM)
        {
            return new Venda
            {
                Id = Guid.NewGuid(),
                DataVenda = DateTime.Now,
                Valor = vendaVM.Produtos.Sum(s => s.ValorTotal),
                Filial = (Filial)vendaVM.Filial,
                Cliente = vendaVM.Cliente,
                Produtos = vendaVM.Produtos.Select(x => (Produto)x).ToList(),
            };
        }
        public static implicit operator VendaCreateViewModel(Venda vendaVM)
        {
            return new VendaCreateViewModel
            {
                Filial = (int)vendaVM.Filial,
                Cliente = vendaVM.Cliente,
                Produtos = vendaVM.Produtos.Select(x => (ProdutoViewModel)x).ToList(),
            };
        }
    }
}
