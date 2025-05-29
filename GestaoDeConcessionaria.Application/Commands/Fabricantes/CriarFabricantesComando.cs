using GestaoDeConcessionaria.Application.DTOs;
using MediatR;

namespace GestaoDeConcessionaria.Application.Commands.Fabricantes
{
    public record CriarFabricantesComando(FabricanteDto Dto) : IRequest<FabricanteDto>;
}
