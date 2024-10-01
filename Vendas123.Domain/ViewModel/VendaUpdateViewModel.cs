using System.Collections.Generic;
using System.Linq;
using Vendas123.Domain.Entites;

namespace Vendas123.Domain.ViewModel
{
    public class VendaUpdateViewModel
    {
        public int CodVenda { get; set; }
        public DateTime DataVenda { get; set; }
        public decimal Valor { get; set; }
        public Filial Filial { get; set; }
        public ClienteViewModel Cliente { get; set; }

        public static implicit operator Venda(VendaUpdateViewModel vendaVM)
        {
            return new Venda
            {
                Id = Guid.NewGuid(),
                CodVenda = vendaVM.CodVenda,
                DataVenda = vendaVM.DataVenda,
                Valor = vendaVM.Valor,
                Filial = vendaVM.Filial,
                Cliente = vendaVM.Cliente,
            };
        }
    }
}
