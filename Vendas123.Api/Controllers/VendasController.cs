using Microsoft.AspNetCore.Mvc;
using Vendas123.Domain.Entites;
using Vendas123.Infrastructure.Contexts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Vendas123.Api.Controllers
{
    [Route("api/vendas")]
    [ApiController]
    public class VendasController : ControllerBase
    {
        private readonly VendasDbContext _context;
        public VendasController(VendasDbContext context)
        {
            this._context = context;
        }
        // GET: api/vendas
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Vendas.ToList());
        }

        // GET api/vendas/5
        [HttpGet("{codVenda}")]
        public IActionResult Get(int codVenda)
        {
            var vendas = _context.Vendas.FirstOrDefault(p => p.CodVenda == codVenda);
            if (vendas == null)
            {
                return NotFound();
            }
            return Ok(vendas);
        }

        // POST api/vendas
        [HttpPost]
        public IActionResult Post([FromBody] Venda venda)
        {
            _context.Vendas.Add(venda);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { codVenda = venda.CodVenda }, venda.CodVenda);
        }

        // PUT api/vendas/5
        [HttpPut("{codVenda}")]
        public IActionResult Put(int codVenda, [FromBody] Venda novaVenda)
        {
            var venda = _context.Vendas.FirstOrDefault(p => p.CodVenda == codVenda);
            if (venda == null)
            {
                return NotFound();
            }

            _context.Vendas.Remove(venda);
            _context.Vendas.Add(novaVenda);
            _context.SaveChanges();
            return NoContent();
        }

        // DELETE api/vendas/5
        [HttpDelete("{codVenda}")]
        public IActionResult Delete(int codVenda)
        {
            var venda = _context.Vendas.FirstOrDefault(p => p.CodVenda == codVenda);
            if (venda == null)
            {
                return NotFound();
            }

            _context.Vendas.Remove(venda);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
