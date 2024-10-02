using Vendas123.Domain.ViewModel;
using Vendas123.Infrastructure.Repositories;

namespace Vendas123.Services.Services
{
    public interface IVendaService
    {
        public List<VendaViewModel> Listar();
        public VendaViewModel GetById(Guid id);
        public VendaViewModel GetByCodVenda(int codVenda);
        public void Save(VendaCreateViewModel vendaVM);
        public bool Update(int codVenda, VendaUpdateViewModel novaVenda);
        public bool Delete(int codVenda);
    }
}
