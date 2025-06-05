using FluentValidation;
using GestaoDeConcessionaria.Application.CQRS.Commands.Auth;

namespace GestaoDeConcessionaria.Application.Validators.Auth
{
    public class LoginUsuarioComandoValidador : AbstractValidator<LoginUsuarioComando>
    {
        public LoginUsuarioComandoValidador()
        {
            RuleFor(x => x.Dto.NomeUsuario)
                .NotEmpty().WithMessage("O nome do usuário é obrigatório.")
                .EmailAddress().WithMessage("O email deve ser válido.");
            RuleFor(x => x.Dto.Senha)
                .NotEmpty().WithMessage("A senha é obrigatória.")
                .MinimumLength(6).WithMessage("A senha deve ter pelo menos 6 caracteres.");
        }
    }
}
