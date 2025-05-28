using GestaoDeConcessionaria.Application.DTOs;
using GestaoDeConcessionaria.Application.Factories;
using GestaoDeConcessionaria.Application.Interfaces;
using MediatR;

namespace GestaoDeConcessionaria.Application.Queries.Vendas
{
    public class BuscarTodasAsVendasHandler(IVendaService svc) : IRequestHandler<BuscarTodasAsVendasQuery, IEnumerable<VendaDetalhesDto>>
    {
        public async Task<IEnumerable<VendaDetalhesDto>> Handle(BuscarTodasAsVendasQuery q, CancellationToken ct)
        {
            var list = await svc.ObterTodasAsVendasAsync();
            return list.Select(v => VendaFactory.CriarDetalhes(v));
        }
    }
}
