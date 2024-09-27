namespace Vendas123.Domain.Entites
{
    public class Venda
    {
        public Guid Id { get; set; }
        public int CodVenda { get; set; }
        public DateTime DataVenda { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime Valor { get; set; }
        public Filial Filial { get; set; }
    }
}
