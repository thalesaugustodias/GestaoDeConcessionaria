using GestaoDeConcessionaria.Application.Factories;
using GestaoDeConcessionaria.Domain.Notificacoes;
using MediatR;
using GestaoDeConcessionaria.Application.DTOs;

namespace GestaoDeConcessionaria.Application.CQRS.Commands.Notificacoes
{
    public class AtualizarNotificacaoHandler(INotificacaoRepository repo) : IRequestHandler<AtualizarNotificacaoComando, NotificacaoDto>
    {
        private readonly INotificacaoRepository _repo = repo;

        public async Task<NotificacaoDto> Handle(AtualizarNotificacaoComando cmd, CancellationToken cancellationToken)
        {
            var existing = await _repo.ObterPorConcessionariaIdAsync(cmd.ConcessionariaId);
            if (existing == null || existing.Id != cmd.Id)
                throw new KeyNotFoundException("Configuração não encontrada.");

            existing = NotificacaoFactory.AtualizarNotificacao(existing, cmd);
            await _repo.AtualizarAsync(existing);

            return NotificacaoFactory.MapearParaDto(existing);
        }
    }
}