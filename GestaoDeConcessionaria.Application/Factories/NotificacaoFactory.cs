using GestaoDeConcessionaria.Application.CQRS.Commands.Notificacoes;
using GestaoDeConcessionaria.Application.DTOs;
using GestaoDeConcessionaria.Domain.Notificacoes;

namespace GestaoDeConcessionaria.Application.Factories
{
    public static class NotificacaoFactory
    {
        public static NotificacaoConfig CriarConfiguracaoNotificacao(CriarNotificacaoComando cmd)
        {
            return new NotificacaoConfig
            {
                ConcessionariaId = cmd.ConcessionariaId,
                VendaCriada = cmd.VendaCriada,
                EstoqueEsgotado = cmd.EstoqueZerado,
                NovoVeiculo = cmd.NovoVeiculo,
                GaragemLotada = cmd.GaragemCheia,
                Emails = cmd.Emails,
                Telefones = cmd.Telefones
            };
        }

        public static NotificacaoConfig AtualizarNotificacao(NotificacaoConfig notificacao, AtualizarNotificacaoComando cmd)
        {
            notificacao.ConcessionariaId = cmd.ConcessionariaId;
            notificacao.VendaCriada = cmd.VendaCriada;
            notificacao.EstoqueEsgotado = cmd.EstoqueZerado;
            notificacao.NovoVeiculo = cmd.NovoVeiculo;
            notificacao.GaragemLotada = cmd.GaragemCheia;
            notificacao.Emails = cmd.Emails;
            notificacao.Telefones = cmd.Telefones;

            return notificacao;
        }

        public static NotificacaoDto MapearParaDto(NotificacaoConfig notificacao)
        {
            return new NotificacaoDto(
                Id: notificacao.Id,
                ConcessionariaId: notificacao.ConcessionariaId,
                VendaCriada: notificacao.VendaCriada,
                EstoqueEsgotado: notificacao.EstoqueEsgotado,
                NovoVeiculo: notificacao.NovoVeiculo,
                GaragemLotada: notificacao.GaragemLotada,
                Emails: notificacao.Emails,
                Telefones: notificacao.Telefones
            );
        }

        public static IEnumerable<NotificacaoDto> MapearListaParaDto(IEnumerable<NotificacaoConfig> notificacoes)
        {
            return notificacoes.Select(notificacao => new NotificacaoDto(
                Id: notificacao.Id!,
                ConcessionariaId: notificacao.ConcessionariaId,
                VendaCriada: notificacao.VendaCriada,
                EstoqueEsgotado: notificacao.EstoqueEsgotado,
                NovoVeiculo: notificacao.NovoVeiculo,
                GaragemLotada: notificacao.GaragemLotada,
                Emails: notificacao.Emails,
                Telefones: notificacao.Telefones
            ));
        }
    }
}
