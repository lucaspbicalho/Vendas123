using System.ComponentModel.DataAnnotations;
using Vendas123.Domain.Entites;

namespace Vendas123.Domain.ViewModel
{
    public class ClienteViewModel
    {
        [DataType(DataType.Text)]
        [Display(Name = "Nome Cliente")]
        public string Nome { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Cpf Cliente")]
        public string Cpf { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Telefone Cliente")]
        public string Telefone { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail Cliente")]
        public string Email { get; set; }
        public static implicit operator Cliente(ClienteViewModel clienteVM)
        {
            return new Cliente
            {
                Id = Guid.NewGuid(),
                Nome = clienteVM.Nome,
                Cpf = clienteVM.Cpf,
                Telefone = clienteVM.Telefone,
                Email = clienteVM.Email,
            };
        }
        public static implicit operator ClienteViewModel(Cliente clienteVM)
        {
            return new ClienteViewModel
            {
                Nome = clienteVM.Nome,
                Cpf = clienteVM.Cpf,
                Telefone = clienteVM.Telefone,
                Email = clienteVM.Email,
            };
        }
    }
}
