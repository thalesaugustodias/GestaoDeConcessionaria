using GestaoDeConcessionaria.Application.DTOs;
using MediatR;

namespace GestaoDeConcessionaria.Application.CQRS.Commands.Concessionarias
{
    public record AtualizarConcessionariaComando(ConcessionariaDto Dto) : IRequest<Unit>;
}
