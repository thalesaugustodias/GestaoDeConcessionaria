using GestaoDeConcessionaria.Domain.Notificacoes;
using MediatR;

namespace GestaoDeConcessionaria.Application.CQRS.Commands.Notificacoes
{
    public class DeletarNotificacaoHandlerDeleteNotificationSettingCommandHandler(INotificacaoRepository repo) : IRequestHandler<DeletarNotificacaoComando, Unit>
    {
        private readonly INotificacaoRepository _repo = repo;

        public async Task<Unit> Handle(DeletarNotificacaoComando cmd,CancellationToken cancellationToken)
        {
            await _repo.ExcluirAsync(cmd.Id);
            return Unit.Value;
        }
    }
}
