using Vendas123.Domain.Entites;

namespace Vendas123.Domain.ViewModel
{
    public class VendaViewModel
    {
        public int CodVenda { get; set; }
        public DateTime DataVenda { get; set; }
        public ClienteViewModel Cliente { get; set; }
        public decimal Valor { get; set; }
        public Filial Filial { get; set; }

        public static implicit operator Venda(VendaViewModel vendaVM)
        {
            return new Venda
            {
                Id = Guid.NewGuid(),
                CodVenda = vendaVM.CodVenda,
                DataVenda = vendaVM.DataVenda,
                Cliente = vendaVM.Cliente,
                Valor = vendaVM.Valor,
                Filial = vendaVM.Filial,
            };
        }
    }
}
