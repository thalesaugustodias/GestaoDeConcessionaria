using GestaoDeConcessionaria.Application.DTOs;
using MediatR;

namespace GestaoDeConcessionaria.Application.Commands.Clientes
{
    public record AtualizarClienteComando : IRequest<Unit>
    {
        public ClienteDto Dto { get; init; }
    }
}
