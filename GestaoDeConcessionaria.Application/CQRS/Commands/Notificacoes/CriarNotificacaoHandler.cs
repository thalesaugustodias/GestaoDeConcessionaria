using GestaoDeConcessionaria.Application.DTOs;
using GestaoDeConcessionaria.Application.Factories;
using GestaoDeConcessionaria.Domain.Notificacoes;
using MediatR;

namespace GestaoDeConcessionaria.Application.CQRS.Commands.Notificacoes
{
    public class CriarNotificacaoHandler(INotificacaoRepository repo) : IRequestHandler<CriarNotificacaoComando, NotificacaoDto>
    {
        private readonly INotificacaoRepository _repo = repo;

        public async Task<NotificacaoDto> Handle(CriarNotificacaoComando cmd, CancellationToken cancellationToken)
        {
            var notificacaoConfig = NotificacaoFactory.CriarConfiguracaoNotificacao(cmd);

            await _repo.CriarAsync(notificacaoConfig);
            return NotificacaoFactory.MapearParaDto(notificacaoConfig);           
        }
    }
}