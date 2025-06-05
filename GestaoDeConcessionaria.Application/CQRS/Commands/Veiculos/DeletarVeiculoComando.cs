using MediatR;

namespace GestaoDeConcessionaria.Application.CQRS.Commands.Veiculos
{
    public record DeletarVeiculoComando(int Id) : IRequest<Unit>;
}
