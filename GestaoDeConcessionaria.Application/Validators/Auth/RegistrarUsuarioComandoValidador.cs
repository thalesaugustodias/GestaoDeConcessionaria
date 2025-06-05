using FluentValidation;
using GestaoDeConcessionaria.Application.CQRS.Commands.Auth;

namespace GestaoDeConcessionaria.Application.Validators.Auth
{
    public class RegistrarUsuarioComandoValidador : AbstractValidator<RegistrarUsuarioComando>
    {
        public RegistrarUsuarioComandoValidador()
        {
            RuleFor(x => x.Dto.NomeUsuario)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres.");
            RuleFor(x => x.Dto.Email)
                .NotEmpty().WithMessage("O email é obrigatório.")
                .EmailAddress().WithMessage("O email deve ser válido.");
            RuleFor(x => x.Dto.Senha)
                .NotEmpty().WithMessage("A senha é obrigatória.")
                .MinimumLength(6).WithMessage("A senha deve ter pelo menos 6 caracteres.");
        }
    }
}
