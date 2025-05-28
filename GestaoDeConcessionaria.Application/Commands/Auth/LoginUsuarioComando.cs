using GestaoDeConcessionaria.Application.DTOs;
using MediatR;

namespace GestaoDeConcessionaria.Application.Commands.Auth
{

    public record LoginUsuarioComando(LoginUsuarioDto Dto) : IRequest<string>;
}
