using System;
using Vendas123.Domain.Entites;

namespace Vendas123.Infrastructure.Contexts
{
    public class FakeContext
    {
        public List<Venda> Vendas;
        public List<Cliente> Clientes;
        
        public FakeContext()
        {
            Vendas = new List<Venda>() { };
            Clientes = new List<Cliente>() { };
        }
    }
}
