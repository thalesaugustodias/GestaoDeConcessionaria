using FluentValidation;
using GestaoDeConcessionaria.Application.Commands.Concessionarias;

namespace GestaoDeConcessionaria.Application.Validators.Concessionarias
{
    public class CriarConcessionariasComandoValidador : AbstractValidator<CriarConcessionariaComando>
    {
        public CriarConcessionariasComandoValidador()
        {
            RuleFor(x => x.Dto.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .MinimumLength(3).WithMessage("O nome deve ter pelo menos 3 caracteres.")
                .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres.");

            RuleFor(x => x.Dto.Rua)
                .NotEmpty().WithMessage("A rua é obrigatória.")
                .MaximumLength(150).WithMessage("A rua deve ter no máximo 150 caracteres.");

            RuleFor(x => x.Dto.Cidade)
                .NotEmpty().WithMessage("A cidade é obrigatória.")
                .MaximumLength(100).WithMessage("A cidade deve ter no máximo 100 caracteres.");

            RuleFor(x => x.Dto.Estado)
                .NotEmpty().WithMessage("O estado é obrigatório.")
                .Length(2).WithMessage("O estado deve ter 2 caracteres (UF).")
                .Matches("^[A-Z]{2}$").WithMessage("O estado deve conter apenas letras maiúsculas.");

            RuleFor(x => x.Dto.CEP)
                .NotEmpty().WithMessage("O CEP é obrigatório.")
                .Length(8).WithMessage("O CEP deve ter 8 dígitos.")
                .Matches("^[0-9]{8}$").WithMessage("O CEP deve conter apenas números.");

            RuleFor(x => x.Dto.Telefone)
                .NotEmpty().WithMessage("O telefone é obrigatório.")
                .Matches(@"^\d{10,11}$").WithMessage("O telefone deve conter 10 ou 11 dígitos numéricos.");

            RuleFor(x => x.Dto.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage("O e-mail informado não é válido.");

            RuleFor(x => x.Dto.CapacidadeMaximaVeiculos)
                .GreaterThan(0).WithMessage("A capacidade máxima de veículos deve ser maior que zero.")
                .LessThanOrEqualTo(10000).WithMessage("A capacidade máxima de veículos deve ser no máximo 10.000.");
        }
    }
}
