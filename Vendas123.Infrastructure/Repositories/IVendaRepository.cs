using Vendas123.Domain.Entites;

namespace Vendas123.Infrastructure.Repositories
{
    public interface IVendaRepository
    {
        public List<Venda> GetAll();
        public Venda GetById(Guid codVenda);
        public Venda GetByCodVenda(int codVenda);
        public void Save(Venda vendaVM);
        public bool Delete(int codVenda);
        public bool Update(int codVenda, Venda novaVenda);
    }
}
