using GestaoDeConcessionaria.Application.DTOs;
using MediatR;

namespace GestaoDeConcessionaria.Application.CQRS.Commands.Concessionarias
{
    public record CriarConcessionariaComando(ConcessionariaDto Dto) : IRequest<ConcessionariaDto>;
}
