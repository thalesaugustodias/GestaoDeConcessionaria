using GestaoDeConcessionaria.Application.DTOs;
using GestaoDeConcessionaria.Domain.Notificacoes;
using MediatR;

namespace GestaoDeConcessionaria.Application.CQRS.Queries.Notificacao
{
    public class BuscarNotificacaoPorIdHandler(INotificacaoRepository repo) : IRequestHandler<BuscarNotificacaoPorIdQuery, NotificacaoDto?>
    {
        private readonly INotificacaoRepository _repo = repo;

        public async Task<NotificacaoDto?> Handle(BuscarNotificacaoPorIdQuery query, CancellationToken cancellationToken)
        {
            var todos = await _repo.ObterTodosAsync();
            var config = todos.FirstOrDefault(ns => ns.Id == query.Id);
            if (config == null) return null;
            return Factories.NotificacaoFactory.MapearParaDto(config);
        }
    }
}
