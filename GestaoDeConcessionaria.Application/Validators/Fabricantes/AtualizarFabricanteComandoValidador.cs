using FluentValidation;
using GestaoDeConcessionaria.Application.CQRS.Commands.Fabricantes;

namespace GestaoDeConcessionaria.Application.Validators.Fabricantes
{
    public class AtualizarFabricanteComandoValidador : AbstractValidator<AtualizarFabricanteComando>
    {
        public AtualizarFabricanteComandoValidador()
        {
            RuleFor(x => x.Dto.Id)
                .GreaterThan(0)
                .WithMessage("O Id do fabricante deve ser maior que zero.");

            RuleFor(x => x.Dto)
                .NotNull()
                .WithMessage("Os dados do fabricante são obrigatórios.");

            When(x => x.Dto != null, () =>
            {
                RuleFor(x => x.Dto.Nome)
                    .NotEmpty()
                    .WithMessage("O nome do fabricante é obrigatório.")
                    .MaximumLength(100)
                    .WithMessage("O nome do fabricante deve ter no máximo 100 caracteres.");

                RuleFor(x => x.Dto.PaisOrigem)
                    .NotEmpty()
                    .WithMessage("O país do fabricante é obrigatório.")
                    .MaximumLength(50)
                    .WithMessage("O país do fabricante deve ter no máximo 50 caracteres.");
            });
        }
    }
}
