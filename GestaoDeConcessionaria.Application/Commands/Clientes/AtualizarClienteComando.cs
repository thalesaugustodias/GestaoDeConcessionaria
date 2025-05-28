using GestaoDeConcessionaria.Application.DTOs;
using MediatR;

namespace GestaoDeConcessionaria.Application.Commands.Clientes
{
    public record AtualizarClienteComando (int Id, ClienteDto Dto) : IRequest<Unit>
    {
        public int Id { get; init; } = Id;
        public ClienteDto Dto { get; init; } = Dto ?? throw new ArgumentNullException(nameof(Dto), "DTO não pode ser nulo");
    }
}
