using Microsoft.EntityFrameworkCore;
using Vendas123.Domain.Entites;

namespace Vendas123.Infrastructure.Contexts
{
    public class VendasDbContext : DbContext
    {
        public VendasDbContext(DbContextOptions<VendasDbContext> options) : base(options)
        {
        }

        public DbSet<Venda> Vendas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
    }
}
