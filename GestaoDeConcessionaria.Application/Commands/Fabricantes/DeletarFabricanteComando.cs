using MediatR;

namespace GestaoDeConcessionaria.Application.Commands.Fabricantes
{
    public record DeletarFabricanteComando(int Id) : IRequest<Unit>;
}
