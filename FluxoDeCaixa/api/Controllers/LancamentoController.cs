using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LancamentoController : ControllerBase
    {
        private readonly LancamentoService _service;

        public LancamentoController(LancamentoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Lancamento lancamento)
        {
            await _service.AdicionarLancamento(lancamento);
            return CreatedAtAction(nameof(Post), lancamento);
        }
    }

}
