using Microsoft.EntityFrameworkCore;
using Vendas123.Domain.Entites;
using Vendas123.Domain.ViewModel;
using Vendas123.Infrastructure.Contexts;

namespace Vendas123.Infrastructure.Repositories
{
    public class VendaRepository : IVendaRepository
    {
        private readonly VendasDbContext _context;

        public VendaRepository(VendasDbContext context)
        {
            _context = context;
        }
        public List<Venda> GetAll()
        {
            return _context.Vendas
                .Include(c => c.Cliente)
                .Include(p => p.Produtos)
                .ToList();
        }
        public Venda GetById(Guid id)
        {
            return _context.Vendas
                .Include(c => c.Cliente)
                .Include(p => p.Produtos)
                .Where(w => w.Id == id)
                .FirstOrDefault();
        }
        public Venda GetByCodVenda(int codVenda)
        {
            DateTime dateNow = DateTime.Now;
            return _context.Vendas
                .Include(c => c.Cliente)
                .Include(p => p.Produtos)
                .Where(w => w.CodVenda == codVenda)
                .FirstOrDefault();
        }
        public void Save(Venda venda)
        {
            _context.Vendas.Add(venda);
            _context.SaveChanges();
        }
        public bool Update(int codVenda, Venda novaVenda)
        {
            var venda = _context.Vendas
                .Include(c => c.Cliente)
                .Include(p => p.Produtos)
                .FirstOrDefault(p => p.CodVenda == codVenda);

            if (venda == null)
            {
                return false;
            }
            else
            {
                //
                venda.Valor = novaVenda.Valor;
                venda.Filial = (Filial)novaVenda.Filial;
                //Cliente
                venda.Cliente.Nome = novaVenda.Cliente.Nome;
                venda.Cliente.Telefone = novaVenda.Cliente.Telefone;
                venda.Cliente.Email = novaVenda.Cliente.Email;
            }
            _context.Clientes.Update(venda.Cliente);
            _context.Vendas.Update(venda);
            _context.SaveChanges();
            return true;
        }
        public bool Delete(int codVenda)
        {
            var venda = _context.Vendas
                    .Include(c => c.Cliente)
                    .Include(p => p.Produtos)
                    .FirstOrDefault(p => p.CodVenda == codVenda);
            if (venda == null)
            {
                return false;
            }
            _context.Clientes.Remove(venda.Cliente);
            _context.Produtos.RemoveRange(venda.Produtos);
            _context.Vendas.Remove(venda);
            _context.SaveChanges();
            return true;
        }
    }
}
