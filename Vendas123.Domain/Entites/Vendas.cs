namespace Vendas123.Domain.Enums
{
    public class Vendas
    {
        public Guid Id { get; set; }
        public string CodVenda { get; set; }
        public DateTime DataVenda { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime Valor { get; set; }
        public Filial Filial { get; set; }
    }
}
