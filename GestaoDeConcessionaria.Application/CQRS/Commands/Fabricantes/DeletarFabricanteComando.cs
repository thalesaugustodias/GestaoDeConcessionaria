using MediatR;

namespace GestaoDeConcessionaria.Application.CQRS.Commands.Fabricantes
{
    public record DeletarFabricanteComando(int Id) : IRequest<Unit>;
}
