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
        private readonly FakeContext _fakeContext;
        public VendasController(FakeContext fakeContext)
        {
            this._fakeContext = fakeContext;
        }
        // GET: api/vendas
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_fakeContext.Vendas.ToList());
        }

        // GET api/vendas/5
        [HttpGet("{codVenda}")]
        public IActionResult Get(int codVenda)
        {
            var vendas = _fakeContext.Vendas.FirstOrDefault(p => p.CodVenda == codVenda);
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
            _fakeContext.Vendas.Add(venda);

            return CreatedAtAction(nameof(Get), new { codVenda = venda.CodVenda }, venda.CodVenda);
        }

        // PUT api/vendas/5
        [HttpPut("{codVenda}")]
        public IActionResult Put(int codVenda, [FromBody] Venda novaVenda)
        {
            var venda = _fakeContext.Vendas.FirstOrDefault(p => p.CodVenda == codVenda);
            if (venda == null)
            {
                return NotFound();
            }

            _fakeContext.Vendas.Remove(venda);
            _fakeContext.Vendas.Add(novaVenda);
            return NoContent();
        }

        // DELETE api/vendas/5
        [HttpDelete("{codVenda}")]
        public IActionResult Delete(int codVenda)
        {
            var venda = _fakeContext.Vendas.FirstOrDefault(p => p.CodVenda == codVenda);
            if (venda == null)
            {
                return NotFound();
            }

            _fakeContext.Vendas.Remove(venda);
            return NoContent();
        }
    }
}
