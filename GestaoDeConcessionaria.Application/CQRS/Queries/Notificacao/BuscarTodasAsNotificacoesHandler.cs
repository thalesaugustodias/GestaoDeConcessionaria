using GestaoDeConcessionaria.Application.Factories;
using GestaoDeConcessionaria.Application.DTOs;
using GestaoDeConcessionaria.Domain.Notificacoes;
using MediatR;

namespace GestaoDeConcessionaria.Application.CQRS.Queries.Notificacao
{
    public class BuscarTodasAsNotificacoesHandler(INotificacaoRepository repo) : IRequestHandler<BuscarTodasAsNotificacoesQuery, IEnumerable<NotificacaoDto>>
    {
        private readonly INotificacaoRepository _repo = repo;

        public async Task<IEnumerable<NotificacaoDto>> Handle(BuscarTodasAsNotificacoesQuery query, CancellationToken cancellationToken)
        {
            var notificacoes = await _repo.ObterTodosAsync();
            return NotificacaoFactory.MapearListaParaDto(notificacoes);
        }
    }
}