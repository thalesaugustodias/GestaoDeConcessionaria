using MediatR;

namespace GestaoDeConcessionaria.Application.Commands.Clientes
{
    public record DeletarClienteComando(int Id) : IRequest<Unit>
    {
        public int Id { get; init; } = Id;
    };
}
