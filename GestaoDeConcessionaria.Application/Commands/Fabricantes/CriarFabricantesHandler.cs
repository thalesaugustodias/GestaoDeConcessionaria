using GestaoDeConcessionaria.Application.DTOs;
using GestaoDeConcessionaria.Application.Factories;
using GestaoDeConcessionaria.Application.Interfaces;
using MediatR;

namespace GestaoDeConcessionaria.Application.Commands.Fabricantes
{
    public class CriarFabricantesHandler(IFabricanteService svc) : IRequestHandler<CriarFabricantesComando, FabricanteDto>
    {
        private readonly IFabricanteService _svc = svc;

        public async Task<FabricanteDto> Handle(CriarFabricantesComando cmd, CancellationToken ct)
        {
            var ent = FabricanteFactory.Criar(cmd.Dto);
            await _svc.AdicionarAsync(ent);
            return new FabricanteDto(ent.Id, ent.Nome, ent.PaisOrigem, ent.AnoFundacao, ent.Website) { Id = ent.Id };
        }
    }
}
