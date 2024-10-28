using Data;
using Microsoft.EntityFrameworkCore;
using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FluxoDeCaixa.Tests
{
    public class SaldoDiarioServiceTests : IDisposable
    {
        private readonly SaldoDiarioService _service;
        private readonly DbContextOptions<FluxoDeCaixaContext> _options;

        public SaldoDiarioServiceTests()
        {
            _options = new DbContextOptionsBuilder<FluxoDeCaixaContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new FluxoDeCaixaContext(_options);
            _service = new SaldoDiarioService(context);
        }

        [Fact]
        public async Task ObterSaldoDiario_DeveRetornarSaldoCorreto()
        {
            // Arrange
            using (var context = new FluxoDeCaixaContext(_options))
            {
                // Adicionando lançamentos para duas datas distintas
                context.Lancamentos.AddRange(new List<Lancamento>
                {
                    new Lancamento { Tipo = "Credito", Valor = 100.00m, Data = new DateTime(2023, 10, 25) },
                    new Lancamento { Tipo = "Debito", Valor = 50.00m, Data = new DateTime(2023, 10, 25) },
                    new Lancamento { Tipo = "Credito", Valor = 200.00m, Data = new DateTime(2023, 10, 26) },
                });
                await context.SaveChangesAsync();
            }

            // Act
            var resultado = await _service.ObterSaldoDiario();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count); // Espera-se 2 datas
            Assert.Equal(50.00m, resultado.First(sd => sd.Data.Date == new DateTime(2023, 10, 25)).Saldo); // Saldo para 25/10
            Assert.Equal(200.00m, resultado.First(sd => sd.Data.Date == new DateTime(2023, 10, 26)).Saldo); // Saldo para 26/10
        }

        public void Dispose()
        {
            using (var context = new FluxoDeCaixaContext(_options))
            {
                context.Database.EnsureDeleted();
            }
        }
    }
}
