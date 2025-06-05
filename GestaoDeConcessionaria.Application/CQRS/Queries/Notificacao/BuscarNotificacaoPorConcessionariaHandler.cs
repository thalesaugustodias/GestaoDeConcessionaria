using GestaoDeConcessionaria.Application.DTOs;
using GestaoDeConcessionaria.Domain.Notificacoes;
using MediatR;

namespace GestaoDeConcessionaria.Application.CQRS.Queries.Notificacao
{
    partial class BuscarNotificacaoPorConcessionariaHandler(INotificacaoRepository repo) : IRequestHandler<BuscarNotificacaoPorConcessionariaQuery, NotificacaoDto?>
    {
        private readonly INotificacaoRepository _repo = repo;

        public async Task<NotificacaoDto?> Handle(BuscarNotificacaoPorConcessionariaQuery query,CancellationToken cancellationToken)
        {
            var config = await _repo.ObterPorConcessionariaIdAsync(query.ConcessionariaId);
            if (config == null) return null;
            return Factories.NotificacaoFactory.MapearParaDto(config);
        }
    }
}
