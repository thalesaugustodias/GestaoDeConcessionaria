using GestaoDeConcessionaria.Application.DTOs;
using MediatR;

namespace GestaoDeConcessionaria.Application.Commands.Concessionarias
{
    public record AtualizarConcessionariaComando(int Id, ConcessionariaDto Dto) : IRequest<Unit>;
}
