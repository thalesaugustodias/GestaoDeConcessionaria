using GestaoDeConcessionaria.Application.DTOs;
using MediatR;

namespace GestaoDeConcessionaria.Application.CQRS.Commands.Notificacoes
{
    public class AtualizarNotificacaoComando : IRequest<NotificacaoDto>
    {
        public string Id { get; init; } = null!;
        public int ConcessionariaId { get; init; }
        public bool VendaCriada { get; init; }
        public bool EstoqueZerado { get; init; }
        public bool NovoVeiculo { get; init; }
        public bool GaragemCheia { get; init; }
        public List<string> Emails { get; init; } = new();
        public List<string> Telefones { get; init; } = new();
    }
}
