using GestaoDeConcessionaria.Application.DTOs;
using MediatR;

namespace GestaoDeConcessionaria.Application.Commands.Fabricantes
{
    public record AtualizarFabricanteComando(FabricanteDto Dto) : IRequest<Unit>;
}