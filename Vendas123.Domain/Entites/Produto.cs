using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vendas123.Domain.Entites
{
    public class Produto
    {
        public Guid Id { get; set; }
        public int CodProduto { get; set; }
        public string Nome { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal Desconto { get; set; }
        public int Quantidade { get; set; }
        public StatusProduto Status { get; set; }
    }
}
