using Microsoft.EntityFrameworkCore;
using Vendas123.Domain.ViewModel;
using Vendas123.Infrastructure.Repositories;

namespace Vendas123.Services.Services
{
    public class VendaService : IVendaService
    {
        private readonly IVendaRepository _vendaRepository;

        public VendaService(IVendaRepository vendaRepository)
        {
            _vendaRepository = vendaRepository;
        }
        public List<VendaViewModel> Listar()
        {
            return _vendaRepository.GetAll().Select(s => new VendaViewModel
            {
                CodVenda = s.CodVenda,
                DataVenda = s.DataVenda,
                Filial = s.Filial.ToString(),
                ValorTotalVenda = s.Valor,
                Cliente = s.Cliente,
                Produtos = s.Produtos.Select(x => (ProdutoViewModel)x).ToList(),

            }).ToList();

        }
        public VendaViewModel GetById(Guid id)
        {
            var venda = _vendaRepository.GetById(id);
            if (venda == null)
                return null;
            return new VendaViewModel()
            {
                CodVenda = venda.CodVenda,
                DataVenda = venda.DataVenda,
                Filial = venda.Filial.ToString(),
                ValorTotalVenda = venda.Valor,
                Cliente = venda.Cliente,
                Produtos = venda.Produtos.Select(x => (ProdutoViewModel)x).ToList(),
            };
        }
        public VendaViewModel GetByCodVenda(int codVenda)
        {
            var venda = _vendaRepository.GetByCodVenda(codVenda);
            if (venda == null)
                return null;
            return new VendaViewModel()
            {
                CodVenda = venda.CodVenda,
                DataVenda = venda.DataVenda,
                Filial = venda.Filial.ToString(),
                ValorTotalVenda = venda.Valor,
                Cliente = venda.Cliente,
                Produtos = venda.Produtos.Select(x => (ProdutoViewModel)x).ToList(),
            };
        }
        public void Save(VendaCreateViewModel vendaVM)
        {
            _vendaRepository.Save(vendaVM);
        }
        public bool Update(int codVenda, VendaUpdateViewModel novaVenda)
        {
            return _vendaRepository.Update(codVenda, novaVenda);
        }
        public bool Delete(int codVenda)
        {
            return _vendaRepository.Delete(codVenda);
        }
    }
}
