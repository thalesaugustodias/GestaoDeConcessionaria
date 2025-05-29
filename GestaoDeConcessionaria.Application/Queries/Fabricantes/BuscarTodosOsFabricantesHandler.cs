using GestaoDeConcessionaria.Application.DTOs;
using GestaoDeConcessionaria.Application.Interfaces;
using MediatR;

namespace GestaoDeConcessionaria.Application.Queries.Fabricantes
{
    public class BuscarTodosOsFabricantesHandler(IFabricanteService svc) : IRequestHandler<BuscarTodosOsFabricantesQuery, IEnumerable<FabricanteDto>>
    {
        private readonly IFabricanteService _svc = svc;

        public async Task<IEnumerable<FabricanteDto>> Handle(BuscarTodosOsFabricantesQuery q, CancellationToken ct)
        {
            var list = await _svc.ObterTodosAsync();
            return list.Select(f =>
                new FabricanteDto(f.Id,f.Nome, f.PaisOrigem, f.AnoFundacao, f.Website) { Id = f.Id }
            );
        }
    }
}
