using MediatR;

namespace GestaoDeConcessionaria.Application.Commands.Concessionarias
{
    public record DeletarConcessionariaComando (int Id) : IRequest<Unit>;
}
