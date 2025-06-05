using FluentValidation;
using GestaoDeConcessionaria.Application.CQRS.Commands.Notificacoes;

namespace GestaoDeConcessionaria.Application.Validators.Notificacao
{
    public class CriarNotificacaoComandoValidador : AbstractValidator<CriarNotificacaoComando>
    {
        public CriarNotificacaoComandoValidador()
        {
            RuleFor(x => x.ConcessionariaId)
                .GreaterThan(0).WithMessage("Concessionária inválida.");

            RuleForEach(x => x.Emails)
                .EmailAddress().WithMessage("Cada e-mail deve ser válido.");

            RuleForEach(x => x.Telefones)
                .Matches(@"^\+?[0-9]{8,15}$")
                .WithMessage("Cada telefone deve conter apenas dígitos (com DDI opcional).");
            RuleFor(x => x)
                .Must(x => x.VendaCriada || x.EstoqueZerado || x.NovoVeiculo || x.GaragemCheia)
                .WithMessage("Ao menos um tipo de notificação deve ser informado.");
            RuleFor(x => x)
                .Must(x => x.Emails != null && x.Emails.Count > 0 || x.Telefones != null && x.Telefones.Count > 0)
                .WithMessage("Pelo menos um meio de contato (e-mail ou telefone) deve ser fornecido.");
        }
    }
}
