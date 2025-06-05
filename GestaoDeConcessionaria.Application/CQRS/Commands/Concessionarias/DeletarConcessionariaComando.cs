using MediatR;

namespace GestaoDeConcessionaria.Application.CQRS.Commands.Concessionarias
{
    public record DeletarConcessionariaComando (int Id) : IRequest<Unit>;
}
