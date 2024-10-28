using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SaldoDiarioController : ControllerBase
    {
        private readonly SaldoDiarioService _service;

        public SaldoDiarioController(SaldoDiarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<SaldoDiario>>> Get()
        {
            var saldoDiario = await _service.ObterSaldoDiario();
            return Ok(saldoDiario);
        }
    }
}
