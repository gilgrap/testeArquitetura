using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Data
{
    public class FluxoDeCaixaContext : DbContext
    {
        public DbSet<Lancamento> Lancamentos { get; set; }
        //public DbSet<SaldoDiario> SaldoDiarios { get; set; }

        public async Task<List<SaldoDiario>> ObterSaldoDiarioAsync()
        {
            return await Lancamentos
                .GroupBy(l => l.Data.Date)
                .Select(g => new SaldoDiario
                {
                    Data = g.Key,
                    Saldo = g.Sum(l => l.Tipo == "Credito" ? l.Valor : -l.Valor)
                })
                .ToListAsync();
        }

        public FluxoDeCaixaContext(DbContextOptions<FluxoDeCaixaContext> options) : base(options) { }
    }

}
