namespace Vendas123.Domain.Entites
{
    public class Venda
    {
        public Guid Id { get; set; }
        public int CodVenda { get; set; }
        public DateTime DataVenda { get; set; }
        public decimal Valor { get; set; }
        public Filial Filial { get; set; }
        public Cliente Cliente { get; set; }
        public List<Produto> Produtos { get; set; }
    }
}
