using GestaoDeConcessionaria.Application.DTOs;
using MediatR;

namespace GestaoDeConcessionaria.Application.CQRS.Commands.Auth
{

    public record LoginUsuarioComando(LoginUsuarioDto Dto) : IRequest<string>;
}
