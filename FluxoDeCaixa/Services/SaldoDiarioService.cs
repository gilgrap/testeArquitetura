using Data;
using Models;

namespace Services
{
    public class SaldoDiarioService
    {
        private readonly FluxoDeCaixaContext _context;

        public SaldoDiarioService(FluxoDeCaixaContext context)
        {
            _context = context;
        }

        public async Task<List<SaldoDiario>> ObterSaldoDiario()
        {
            return await _context.ObterSaldoDiarioAsync();
        }
    }
}
