using FluentValidation;
using GestaoDeConcessionaria.Application.CQRS.Commands.Veiculos;

namespace GestaoDeConcessionaria.Application.Validators.Veiculos
{
    public class AtualizarVeiculoComandoValidador : AbstractValidator<AtualizarVeiculoComando>
    {
        public AtualizarVeiculoComandoValidador()
        {
            RuleFor(x => x.Dto.Id)
                .GreaterThan(0)
                .WithMessage("ID deve ser maior que zero");

            RuleFor(x => x.Dto)
                .NotNull().WithMessage("Os dados do veículo são obrigatórios!")
                .DependentRules(() =>
                {
                    RuleFor(x => x.Dto.Modelo)
                        .NotEmpty().WithMessage("Modelo é obrigatório")
                        .MinimumLength(3).WithMessage("Modelo deve ter no mínimo 3 caracteres")
                        .MaximumLength(50).WithMessage("Modelo deve ter no máximo 50 caracteres");

                    RuleFor(x => x.Dto.AnoFabricacao)
                        .InclusiveBetween(1900, DateTime.Now.Year)
                        .WithMessage($"Ano de fabricação deve estar entre 1900 e {DateTime.Now.Year}");

                    RuleFor(x => x.Dto.Preco)
                        .GreaterThan(0).WithMessage("Preço deve ser maior que zero");

                    RuleFor(x => x.Dto.Tipo)
                        .NotEmpty().WithMessage("Tipo é obrigatório")
                        .MaximumLength(30).WithMessage("Tipo deve ter no máximo 30 caracteres");

                    RuleFor(x => x.Dto.Descricao)
                        .MaximumLength(200).WithMessage("Descrição deve ter no máximo 200 caracteres");

                    RuleFor(x => x.Dto.FabricanteId)
                        .GreaterThan(0).WithMessage("Fabricante inválido");

                    RuleFor(x => x.Dto.NomeFabricante)
                        .NotEmpty().WithMessage("Nome do fabricante é obrigatório")
                        .MaximumLength(50).WithMessage("Nome do fabricante deve ter no máximo 50 caracteres");
                });
        }
    }
}
