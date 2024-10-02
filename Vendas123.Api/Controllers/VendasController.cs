using Microsoft.AspNetCore.Mvc;
using Vendas123.Domain.ViewModel;
using Vendas123.Services.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Vendas123.Api.Controllers
{
    [Route("api/vendas")]
    [ApiController]
    public class VendasController : ControllerBase
    {
        private readonly VendaService _vendasService;
        private readonly ILogger<VendasController> _logger;
        private readonly MessageBrokerService _rabbitMq;
        public VendasController(VendaService vendasService, ILogger<VendasController> logger)
        {
            this._vendasService = vendasService;
            _logger = logger;
            _rabbitMq = new MessageBrokerService() { };
        }
        // GET: api/vendas
        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("{api/vendas/Get} Iniciando.");
            var vendas = _vendasService.Listar();
            _logger.LogInformation("{api/vendas/Get} Fim.");
            return Ok(vendas);
        }

        // GET api/vendas/5
        [HttpGet("{codVenda}")]
        public IActionResult Get(int codVenda)
        {
            _logger.LogInformation("{api/vendas/Get codVenda} Iniciando.", codVenda);
            var venda = _vendasService.GetByCodVenda(codVenda);

            if (venda == null)
            {
                return NotFound();
            }
            _logger.LogInformation("{api/vendas/Get codVenda} Fim.", codVenda);
            return Ok(venda);
        }

        // POST api/vendas
        [HttpPost]
        public IActionResult Post([FromBody] VendaCreateViewModel vendaVM)
        {
            _logger.LogInformation("{api/vendas/Post} Iniciando.",vendaVM);
            _vendasService.Save(vendaVM);
            _logger.LogInformation("{api/vendas/Post} Fim.", vendaVM);
            //send msg to rabbitMq
            _rabbitMq.PostMsg("Compra criada com sucesso!", Eventos.CompraCriada);
            return Created();
        }

        // PUT api/vendas/5
        [HttpPut("{codVenda}")]
        public IActionResult Put(int codVenda, [FromBody] VendaUpdateViewModel novaVenda)
        {
            _logger.LogInformation("{api/vendas/Put} Iniciando.", novaVenda);
            var venda = _vendasService.Update(codVenda, novaVenda);
            if (venda)
            {
                return NoContent();

            }
            _logger.LogInformation("{api/vendas/Put} Fim.", novaVenda);

            //send msg to rabbitMq
            _rabbitMq.PostMsg("Compra alterada com sucesso!", Eventos.CompraAlterada);
            return NotFound();
        }

        // DELETE api/vendas/5
        [HttpDelete("{codVenda}")]
        public IActionResult Delete(int codVenda)
        {
            _logger.LogInformation("{api/vendas/Delete} Iniciando.", codVenda);
            var venda = _vendasService.Delete(codVenda);
            if (venda)
            {
                return NoContent();

            }
            _logger.LogInformation("{api/vendas/Delete codVenda} Fim.", codVenda);

            //send msg to rabbitMq
            _rabbitMq.PostMsg("Compra cancelada com sucesso!", Eventos.CompraCancelada);
            return NotFound();
        }        
    }
}
