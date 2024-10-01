using System.ComponentModel.DataAnnotations;
using Vendas123.Domain.Entites;

namespace Vendas123.Domain.ViewModel
{
    public class ProdutoViewModel
    {
        public int CodProduto { get; set; }
        public string Nome { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal Desconto { get; set; }
        public StatusProduto Status { get; set; }
        public static implicit operator Produto(ProdutoViewModel produtoVM)
        {
            return new Produto
            {
                Id = Guid.NewGuid(),
                CodProduto = produtoVM.CodProduto,
                Nome = produtoVM.Nome,
                Desconto = produtoVM.Desconto,
                Status = produtoVM.Status,
            };
        }
        public static implicit operator ProdutoViewModel(Produto produtoVM)
        {
            return new ProdutoViewModel
            {
                CodProduto = produtoVM.CodProduto,
                Nome = produtoVM.Nome,
                Desconto = produtoVM.Desconto,
                Status = produtoVM.Status,
            };
        }
    }
}
