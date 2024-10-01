using Microsoft.AspNetCore.Mvc;
using Vendas123.Domain.Entites;
using Vendas123.Domain.ViewModel;
using Vendas123.Infrastructure.Contexts;
using Vendas123.Services.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Vendas123.Api.Controllers
{
    [Route("api/vendas")]
    [ApiController]
    public class VendasController : ControllerBase
    {
        private readonly VendaService _vendasService;
        public VendasController(VendaService vendasService)
        {
            this._vendasService = vendasService;
        }
        // GET: api/vendas
        [HttpGet]
        public IActionResult Get()
        {
            var vendas = _vendasService.Listar();
            return Ok(vendas);
        }

        // GET api/vendas/5
        [HttpGet("{codVenda}")]
        public IActionResult Get(int codVenda)
        {
            var venda = _vendasService.GetByCodVenda(codVenda);

            if (venda == null)
            {
                return NotFound();
            }
            return Ok(venda);
        }

        // POST api/vendas
        [HttpPost]
        public IActionResult Post([FromBody] VendaViewModel vendaVM)
        {
            _vendasService.Save(vendaVM);
            return CreatedAtAction(nameof(Get), new { codVenda = vendaVM.CodVenda }, vendaVM.CodVenda);
        }

        // PUT api/vendas/5
        [HttpPut("{codVenda}")]
        public IActionResult Put(int codVenda, [FromBody] VendaUpdateViewModel novaVenda)
        {

            var venda = _vendasService.Update(codVenda, novaVenda);
            if (venda)
            {
                return NoContent();

            }
            return NotFound();
        }

        // DELETE api/vendas/5
        [HttpDelete("{codVenda}")]
        public IActionResult Delete(int codVenda)
        {
            var venda = _vendasService.Delete(codVenda);
            if (venda)
            {
                return NoContent();

            }
            return NotFound();
        }
    }
}
