using MediatR;

namespace GestaoDeConcessionaria.Application.Commands.Veiculos
{
    public record DeletarVeiculoComando(int Id) : IRequest<Unit>;
}
