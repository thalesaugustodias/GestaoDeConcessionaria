using MediatR;

namespace GestaoDeConcessionaria.Application.CQRS.Commands.Notificacoes
{
    public class DeletarNotificacaoComando : IRequest<Unit>
    {
        public string Id { get; init; } = null!;
    }
}