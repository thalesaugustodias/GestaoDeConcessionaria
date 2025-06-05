using FluentValidation;
using GestaoDeConcessionaria.Application.CQRS.Commands.Veiculos;

namespace GestaoDeConcessionaria.Application.Validators.Veiculos
{
    public class CriarVeiculosComandoValidador : AbstractValidator<CriarVeiculosComando>
    {
        public CriarVeiculosComandoValidador()
        {
            RuleFor(x => x.Dto.Modelo)
                .NotEmpty().WithMessage("O campo Modelo é obrigatório.")
                .MinimumLength(3).WithMessage("O Modelo deve ter pelo menos 3 caracteres.")
                .MaximumLength(100).WithMessage("O Modelo deve ter no máximo 100 caracteres.");

            RuleFor(x => x.Dto.AnoFabricacao)
                .InclusiveBetween(1900, DateTime.Now.Year)
                .WithMessage($"O Ano de Fabricação deve ser entre 1900 e {DateTime.Now.Year}.");

            RuleFor(x => x.Dto.Preco)
                .GreaterThan(0).WithMessage("O Preço deve ser maior que zero.");

            RuleFor(x => x.Dto.Tipo)
                .NotEmpty().WithMessage("O campo Tipo é obrigatório.")
                .MinimumLength(3).WithMessage("O Tipo deve ter pelo menos 3 caracteres.");

            RuleFor(x => x.Dto.FabricanteId)
                .GreaterThan(0).WithMessage("O FabricanteId deve ser um valor positivo maior que zero.");
        }
    }
}
