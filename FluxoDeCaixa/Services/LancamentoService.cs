using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class LancamentoService
    {
        private readonly FluxoDeCaixaContext _context;

        public LancamentoService(FluxoDeCaixaContext context)
        {
            _context = context;
        }

        public async Task AdicionarLancamento(Lancamento lancamento)
        {
            _context.Lancamentos.Add(lancamento);
            await _context.SaveChangesAsync();
        }
    }

}
