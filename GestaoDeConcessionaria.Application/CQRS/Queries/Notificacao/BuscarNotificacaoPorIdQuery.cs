using GestaoDeConcessionaria.Application.DTOs;
using MediatR;

namespace GestaoDeConcessionaria.Application.CQRS.Queries.Notificacao
{
    public class BuscarNotificacaoPorIdQuery : IRequest<NotificacaoDto?>
    {
        public string Id { get; init; } = null!;
    }    
}
