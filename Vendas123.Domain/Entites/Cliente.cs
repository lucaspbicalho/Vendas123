using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vendas123.Domain.Entites
{
    public class Cliente
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        [Key]
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
    }
}
