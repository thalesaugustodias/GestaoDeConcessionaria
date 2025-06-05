using FluentValidation;
using GestaoDeConcessionaria.Application.CQRS.Commands.Clientes;
using GestaoDeConcessionaria.Application.Extensions;

namespace GestaoDeConcessionaria.Application.Validators.Clientes
{
    public class AtualizarClienteComandoValidador : AbstractValidator<AtualizarClienteComando>
    {
        public AtualizarClienteComandoValidador()
        {
            RuleFor(x => x.Dto.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres.");
            RuleFor(x => x.Dto.CPF)
                .NotEmpty().WithMessage("O CPF é obrigatório.")
                .Cpf().WithMessage("O CPF deve ser válido.");
            RuleFor(x => x.Dto.Telefone)
                .NotEmpty().WithMessage("O telefone é obrigatório.")
                .Matches(@"^\d{10,11}$").WithMessage("O telefone deve ter 10 ou 11 dígitos.");
        }
    }
}
