using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Vendas123.Domain.Entites;

namespace Vendas123.Domain.ViewModel
{
    public class ProdutoViewModel
    {
        public int CodProduto { get; set; }
        public string Nome { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Range(0.01, Double.MaxValue, ErrorMessage = "O campo {0} precisa ser maior que {0.01}.")]
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get; set; }
        public int quantidade { get; set; }
        public decimal Desconto { get; set; }
        [Range(0, 1, ErrorMessage = "O campo {0} precisa ser entre 0 e 1.")]
        [DefaultValue(0)]
        public int Status { get; set; }
        public static implicit operator Produto(ProdutoViewModel produtoVM)
        {
            return new Produto
            {
                Id = Guid.NewGuid(),
                CodProduto = produtoVM.CodProduto,
                Nome = produtoVM.Nome,
                ValorUnitario = produtoVM.ValorUnitario,
                Quantidade = produtoVM.quantidade,
                Desconto = produtoVM.Desconto,
                Status = (StatusProduto)produtoVM.Status,
            };
        }
        public static implicit operator ProdutoViewModel(Produto produtoVM)
        {
            return new ProdutoViewModel
            {
                CodProduto = produtoVM.CodProduto,
                Nome = produtoVM.Nome,
                ValorUnitario = produtoVM.ValorUnitario,
                quantidade = produtoVM.Quantidade,
                Desconto = produtoVM.Desconto,
                ValorTotal = (produtoVM.Quantidade * produtoVM.ValorUnitario) - produtoVM.Desconto,
                Status = (int)produtoVM.Status,
            };
        }
    }
}
