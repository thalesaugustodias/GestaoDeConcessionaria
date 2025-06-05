using GestaoDeConcessionaria.Application.DTOs;
using MediatR;

namespace GestaoDeConcessionaria.Application.CQRS.Commands.Auth
{
    public record RegistrarUsuarioComando : IRequest<Unit> {
        public RegistrarUsuarioDto? Dto { get; init; }
    }
}
