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
        public List<VendaViewModel> GetAll()
        {
            DateTime dateNow = DateTime.Now;
            return _context.Vendas.Select(s => new VendaViewModel { CodVenda = s.CodVenda, DataVenda = s.DataVenda, Filial = s.Filial, Valor = s.Valor, Cliente = s.Cliente }).ToList();
        }
        public VendaViewModel GetById(Guid id)
        {
            DateTime dateNow = DateTime.Now;
            return _context.Vendas
                .Where(w => w.Id == id)
                .Select(s => new VendaViewModel { CodVenda = s.CodVenda, DataVenda = s.DataVenda, Filial = s.Filial, Valor = s.Valor, Cliente = s.Cliente })
                .FirstOrDefault();
        }
        public VendaViewModel GetByCodVenda(int codVenda)
        {
            DateTime dateNow = DateTime.Now;
            return _context.Vendas
                .Where(w => w.CodVenda == codVenda)
                .Select(s => new VendaViewModel { CodVenda = s.CodVenda, DataVenda = s.DataVenda, Filial = s.Filial, Valor = s.Valor, Cliente = s.Cliente })
                .FirstOrDefault();
        }
        public void Save(VendaViewModel vendaVM)
        {
            _context.Vendas.Add(vendaVM);
            _context.SaveChanges();
        }
        public bool Update(int codVenda, VendaViewModel novaVenda)
        {
            var venda = _context.Vendas
                .FirstOrDefault(p => p.CodVenda == codVenda);

            if (venda == null)
            {
                return false;
            }
            else
            {
                venda.DataVenda = novaVenda.DataVenda;
                venda.Valor = novaVenda.Valor;
                venda.Filial = novaVenda.Filial;
            }

            _context.Vendas.Update(venda);
            _context.SaveChanges();
            return true;
        }
        public bool Delete(int codVenda)
        {
            var venda = _context.Vendas.FirstOrDefault(p => p.CodVenda == codVenda);
            if (venda == null)
            {
                return false;
            }
            _context.Vendas.Remove(venda);
            _context.SaveChanges();
            return true;
        }
    }
}
