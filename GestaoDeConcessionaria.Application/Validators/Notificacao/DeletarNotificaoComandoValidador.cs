using FluentValidation;
using GestaoDeConcessionaria.Application.CQRS.Commands.Notificacoes;

namespace GestaoDeConcessionaria.Application.Validators.Notificacao
{
    public class DeletarNotificaoComandoValidador
        : AbstractValidator<DeletarNotificacaoComando>
    {
        public DeletarNotificaoComandoValidador()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id obrigatório.");
        }
    }
}
