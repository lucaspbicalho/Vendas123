using System.ComponentModel.DataAnnotations;
using Vendas123.Domain.Entites;

namespace Vendas123.Domain.ViewModel
{
    public class VendaViewModel
    {
        public int CodVenda { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DataVenda { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(0.01, Double.MaxValue, ErrorMessage = "O campo {0} precisa ser maior que {0.01}.")]
        public decimal ValorTotalVenda { get; set; }

        public string Filial { get; set; }
        public ClienteViewModel Cliente { get; set; }
        public List<ProdutoViewModel> Produtos { get; set; }

        public static implicit operator Venda(VendaViewModel vendaVM)
        {
            return new Venda
            {
                Id = Guid.NewGuid(),
                CodVenda = vendaVM.CodVenda,
                DataVenda = vendaVM.DataVenda,
                Valor = vendaVM.ValorTotalVenda,
                Filial = (Filial)Enum.Parse(typeof(Filial), vendaVM.Filial),
                Cliente = vendaVM.Cliente,
                Produtos = vendaVM.Produtos.Select(x => (Produto)x).ToList(),
            };
        }
    }
}
