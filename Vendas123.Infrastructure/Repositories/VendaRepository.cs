﻿using Microsoft.EntityFrameworkCore;
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
            return _context.Vendas
                .Include(c => c.Cliente)
                .Include(p => p.Produtos)
                .Select(s => new VendaViewModel
                {
                    CodVenda = s.CodVenda,
                    DataVenda = s.DataVenda,
                    Filial = s.Filial.ToString(),
                    ValorTotalVenda = s.Valor,
                    Cliente = s.Cliente,
                    Produtos = s.Produtos.Select(x => (ProdutoViewModel)x).ToList(),

                })
                .ToList();
        }
        public VendaViewModel GetById(Guid id)
        {
            return _context.Vendas
                .Where(w => w.Id == id)
                .Select(s => new VendaViewModel
                {
                    CodVenda = s.CodVenda,
                    DataVenda = s.DataVenda,
                    Filial = s.Filial.ToString(),
                    ValorTotalVenda = s.Valor,
                    Cliente = s.Cliente,
                    Produtos = s.Produtos.Select(x => (ProdutoViewModel)x).ToList(),
                })
                .FirstOrDefault();
        }
        public VendaViewModel GetByCodVenda(int codVenda)
        {
            DateTime dateNow = DateTime.Now;
            return _context.Vendas
                .Where(w => w.CodVenda == codVenda)
                .Select(s => new VendaViewModel
                {
                    CodVenda = s.CodVenda,
                    DataVenda = s.DataVenda,
                    Filial = s.Filial.ToString(),
                    ValorTotalVenda = s.Valor,
                    Cliente = s.Cliente,
                    Produtos = s.Produtos.Select(x => (ProdutoViewModel)x).ToList(),
                })
                .FirstOrDefault();
        }
        public void Save(VendaCreateViewModel vendaVM)
        {
            _context.Vendas.Add(vendaVM);
            _context.SaveChanges();
        }
        public bool Update(int codVenda, VendaUpdateViewModel novaVenda)
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
