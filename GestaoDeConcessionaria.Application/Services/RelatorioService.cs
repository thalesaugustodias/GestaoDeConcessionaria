using GestaoDeConcessionaria.Application.DTOs;
using GestaoDeConcessionaria.Application.Extensions;
using GestaoDeConcessionaria.Application.Factories;
using GestaoDeConcessionaria.Application.Interfaces;

namespace GestaoDeConcessionaria.Application.Services
{
    public class RelatorioService(
        IVendaService vendaService,
        IClienteService clienteService,
        IVeiculoService veiculoService) : IRelatorioService
    {
        private readonly IVendaService _vendaService = vendaService;
        private readonly IClienteService _clienteService = clienteService;
        private readonly IVeiculoService _veiculoService = veiculoService;

        public async Task<DashboardDto> GerarRelatorioMensalAsync(int mes, int ano)
        {
            var todasVendas = await _vendaService.ObterTodasAsVendasAsync();
            var todasVeiculos = await _veiculoService.ObterTodosAsync();
            var todosClientes = await _clienteService.ObterTodosAsync();

            var vendasDoPeriodo = todasVendas
                .Where(v => v.DataVenda.Month == mes && v.DataVenda.Year == ano)
                .ToList();

            return DashboardDtoFactory.Criar(
                totalVendas: vendasDoPeriodo.Count,
                faturamento: vendasDoPeriodo.Sum(v => v.PrecoVenda),
                vendasPorTipo: vendasDoPeriodo.ToDataPoints(v => v.Veiculo.Tipo.ToString()),
                vendasPorFabricante: vendasDoPeriodo.ToDataPoints(v => v.Veiculo.Fabricante.Nome),
                desempenhoConcessionarias: vendasDoPeriodo.ToDataPoints(v => v.Concessionaria.Nome),
                vendasPorDia: vendasDoPeriodo.ToDataPoints(v => v.DataVenda.Day.ToString("D2")),
                totalVeiculosAtivos: todasVeiculos.Count(),
                totalClientesAtivos: todosClientes.Count()
            );
        }
    }
}
