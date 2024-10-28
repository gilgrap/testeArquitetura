using Data;
using Microsoft.EntityFrameworkCore;
using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace FluxoDeCaixa.Tests
{
    public class LancamentoServiceTests : IDisposable
    {
        private readonly LancamentoService _service;
        private readonly DbContextOptions<FluxoDeCaixaContext> _options;

        public LancamentoServiceTests()
        {
            _options = new DbContextOptionsBuilder<FluxoDeCaixaContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Cria um novo banco para cada teste
                .Options;

            var context = new FluxoDeCaixaContext(_options);
            _service = new LancamentoService(context);
        }

        [Fact]
        public async Task AdicionarLancamento_DeveAdicionarLancamentoCorretamente()
        {
            // Arrange
            var lancamento = new Lancamento
            {
                Tipo = "Credito",
                Valor = 100.00m,
                Data = DateTime.Now
            };

            // Act
            await _service.AdicionarLancamento(lancamento);

            // Assert
            using (var context = new FluxoDeCaixaContext(_options))
            {
                var lancamentos = await context.Lancamentos.ToListAsync();
                Assert.Single(lancamentos);
                Assert.Equal(lancamento.Valor, lancamentos[0].Valor);
            }
        }

        public void Dispose()
        {
            // Limpa o banco de dados após cada teste
            using (var context = new FluxoDeCaixaContext(_options))
            {
                context.Database.EnsureDeleted();
            }
        }
    }
}
