using GestaoDeConcessionaria.Application.DTOs;
using MediatR;

namespace GestaoDeConcessionaria.Application.CQRS.Queries.Notificacao
{
    public class BuscarNotificacaoPorConcessionariaQuery : IRequest<NotificacaoDto?>
    {
        public int ConcessionariaId { get; init; }
    }
}
