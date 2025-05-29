using GestaoDeConcessionaria.Application.DTOs;
using MediatR;

namespace GestaoDeConcessionaria.Application.Commands.Concessionarias
{
    public record CriarConcessionariaComando(ConcessionariaDto Dto) : IRequest<ConcessionariaDto>;
}
