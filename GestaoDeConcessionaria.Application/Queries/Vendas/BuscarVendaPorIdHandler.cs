using GestaoDeConcessionaria.Application.DTOs;
using GestaoDeConcessionaria.Application.Factories;
using GestaoDeConcessionaria.Application.Interfaces;
using MediatR;

namespace GestaoDeConcessionaria.Application.Queries.Vendas
{
    public class BuscarVendaPorIdHandler(IVendaService svc) : IRequestHandler<BuscarVendaPorIdQuery, VendaDetalhesDto>
    {
        private readonly IVendaService _svc = svc;

        public async Task<VendaDetalhesDto> Handle(BuscarVendaPorIdQuery q, CancellationToken ct)
        {
            var v = await Task.FromResult(_svc.ObterPorIdAsync(q.Id))
                ?? throw new KeyNotFoundException("Venda não encontrada");
            return VendaFactory.CriarDetalhes(v);
        }
    }
}
