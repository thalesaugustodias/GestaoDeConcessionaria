using FluentValidation;
using GestaoDeConcessionaria.Application.Commands.Vendas;

namespace GestaoDeConcessionaria.Application.Validators.Vendas
{
    public class CriarVendasComandoValidador : AbstractValidator<CriarVendasComando>
    {
        public CriarVendasComandoValidador()
        {
            RuleFor(x => x.Dto.ClienteId)
                .NotEmpty().WithMessage("O ID do cliente é obrigatório.");
            RuleFor(x => x.Dto.VeiculoId)
                .NotEmpty().WithMessage("O ID do veículo é obrigatório.");
            RuleFor(x => x.Dto.DataVenda)
                .NotEmpty().WithMessage("A data da venda é obrigatória.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("A data da venda não pode ser futura.");
            RuleFor(x => x.Dto.PrecoVenda)
                .GreaterThan(0).WithMessage("O valor total deve ser maior que zero.");
        }
    }
}
