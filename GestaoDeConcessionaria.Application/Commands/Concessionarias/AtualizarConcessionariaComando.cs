using GestaoDeConcessionaria.Application.DTOs;
using MediatR;

namespace GestaoDeConcessionaria.Application.Commands.Concessionarias
{
    public record AtualizarConcessionariaComando(ConcessionariaDto Dto) : IRequest<Unit>;
}
