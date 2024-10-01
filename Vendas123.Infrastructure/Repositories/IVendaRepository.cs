using Vendas123.Domain.Entites;
using Vendas123.Domain.ViewModel;

namespace Vendas123.Infrastructure.Repositories
{
    public interface IVendaRepository
    {
        public List<VendaViewModel> GetAll();
        public VendaViewModel GetById(Guid codVenda);
        public VendaViewModel GetByCodVenda(int codVenda);
        public void Save(VendaViewModel vendaVM);
        public bool Delete(int codVenda);
        public bool Update(int codVenda, VendaUpdateViewModel novaVenda);
    }
}
