using System.ComponentModel.DataAnnotations;
using Vendas123.Domain.Entites;

namespace Vendas123.Domain.ViewModel
{
    public class ClienteViewModel
    {
        public int CodCliente { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public static implicit operator Cliente(ClienteViewModel clienteVM)
        {
            return new Cliente
            {
                Id = Guid.NewGuid(),
                CodCliente = clienteVM.CodCliente,
                Name = clienteVM.Name,
                Cpf = clienteVM.Cpf,
                Telefone = clienteVM.Telefone,
                Email = clienteVM.Email,
            };
        }
        public static implicit operator ClienteViewModel(Cliente clienteVM)
        {
            return new ClienteViewModel
            {
                Name = clienteVM.Name,
                Telefone = clienteVM.Telefone,
                Email = clienteVM.Email,
            };
        }
    }
}
