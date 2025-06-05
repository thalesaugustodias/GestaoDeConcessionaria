using GestaoDeConcessionaria.Application.DTOs;
using MediatR;

namespace GestaoDeConcessionaria.Application.CQRS.Queries.Notificacao
{
    public class BuscarTodasAsNotificacoesQuery : IRequest<IEnumerable<NotificacaoDto>>
    {
    }
}
