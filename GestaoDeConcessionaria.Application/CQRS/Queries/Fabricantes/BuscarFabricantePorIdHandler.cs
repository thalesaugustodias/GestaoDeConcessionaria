using GestaoDeConcessionaria.Application.DTOs;
using GestaoDeConcessionaria.Application.Interfaces;
using MediatR;

namespace GestaoDeConcessionaria.Application.CQRS.Queries.Fabricantes
{
    public class BuscarFabricantePorIdHandler(IFabricanteService svc) : IRequestHandler<BuscarFabricantePorIdQuery, FabricanteDto>
    {
        private readonly IFabricanteService _svc = svc;

        public async Task<FabricanteDto> Handle(BuscarFabricantePorIdQuery q, CancellationToken ct)
        {
            var f = await _svc.ObterPorIdAsync(q.Id)
                ?? throw new KeyNotFoundException("Fabricante não encontrado");
            return new FabricanteDto(f.Id, f.Nome, f.PaisOrigem, f.AnoFundacao, f.Website) { Id = f.Id };
        }
    }
}
