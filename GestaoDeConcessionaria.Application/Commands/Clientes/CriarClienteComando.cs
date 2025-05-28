using GestaoDeConcessionaria.Application.DTOs;
using MediatR;

namespace GestaoDeConcessionaria.Application.Commands.Clientes
{
    public record CriarClienteComando(ClienteDto Dto) : IRequest<ClienteDto>;
}
