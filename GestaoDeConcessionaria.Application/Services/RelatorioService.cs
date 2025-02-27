﻿using GestaoDeConcessionaria.Application.DTOs;
using GestaoDeConcessionaria.Application.Interfaces;

namespace GestaoDeConcessionaria.Application.Services
{
    public class RelatorioService(IVendaService vendaService, IClienteService clienteService, IVeiculoService veiculoService) : IRelatorioService
    {
        private readonly IVendaService _vendaService = vendaService;
        private readonly IClienteService _clienteService = clienteService;
        private readonly IVeiculoService _veiculoService = veiculoService;

        public async Task<DashboardDto> GerarRelatorioMensalAsync(int mes, int ano)
        {
            var vendas = await _vendaService.ObterTodasAsVendasAsync();
            var vendasMensais = vendas.Where(v => v.DataVenda.Month == mes && v.DataVenda.Year == ano).ToList();
            var totalVeiculosAtivos = _veiculoService.ObterTodosAsync().Result;
            var totalClientesAtivos = _clienteService.ObterTodosAsync().Result;

            if (vendasMensais.Count == 0)
                throw new Exception("Nenhuma venda encontrada para o período informado.");

            var totalVendas = vendasMensais.Count;
            var faturamento = vendasMensais.Sum(v => v.PrecoVenda);

            var vendasPorTipo = vendasMensais.GroupBy(v => v.Veiculo.Tipo)
                .Select(g => new DataPoint(g.Key.ToString(), g.Count()))
                .ToList();

            var vendasPorFabricante = vendasMensais.GroupBy(v => v.Veiculo.Fabricante.Nome)
                .Select(g => new DataPoint(g.Key, g.Count()))
                .ToList();

            var desempenhoConcessionarias = vendasMensais.GroupBy(v => v.Concessionaria.Nome)
                .Select(g => new DataPoint(g.Key, g.Count()))
                .ToList();

            var vendasPorDia = vendasMensais
                .GroupBy(v => v.DataVenda.Day)
                .Select(g => new DataPoint(g.Key.ToString("D2"), g.Count()))
                .OrderBy(dp => int.Parse(dp.Label))
                .ToList();

            return new DashboardDto(totalVendas, faturamento, vendasPorTipo, vendasPorFabricante, desempenhoConcessionarias, vendasPorDia, totalVeiculosAtivos.Count(), totalClientesAtivos.Count());
        }

    }
}
