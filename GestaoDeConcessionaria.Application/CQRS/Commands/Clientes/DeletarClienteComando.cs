using MediatR;

namespace GestaoDeConcessionaria.Application.CQRS.Commands.Clientes
{
    public record DeletarClienteComando(int Id) : IRequest<Unit>
    {
        public int Id { get; init; } = Id;
    };
}
