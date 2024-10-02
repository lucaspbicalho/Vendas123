﻿using Vendas123.Domain.ViewModel;
using Vendas123.Infrastructure.Repositories;

namespace Vendas123.Services.Services
{
    public class VendaService: IVendaService
    {
        private readonly IVendaRepository _vendaRepository;

        public VendaService(IVendaRepository vendaRepository)
        {
            _vendaRepository = vendaRepository;
        }
        public List<VendaViewModel> Listar()
        {
            return _vendaRepository.GetAll();
        }
        public VendaViewModel GetById(Guid id)
        {
            var venda = _vendaRepository.GetById(id);
            return venda;
        }
        public VendaViewModel GetByCodVenda(int codVenda)
        {
            var venda = _vendaRepository.GetByCodVenda(codVenda);
            return venda;
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
