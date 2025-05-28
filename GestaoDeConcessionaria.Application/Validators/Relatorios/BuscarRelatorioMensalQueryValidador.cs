using FluentValidation;
using GestaoDeConcessionaria.Application.Queries.Relatorios;

namespace GestaoDeConcessionaria.Application.Validators.Relatorios
{
    public class BuscarRelatorioMensalQueryValidador : AbstractValidator<BuscarRelatorioMensalQuery>
    {
        public BuscarRelatorioMensalQueryValidador()
        {
            RuleFor(x => x.mes)
                .NotEmpty().WithMessage("O mês é obrigatório.")
                .InclusiveBetween(1, 12).WithMessage("O mês deve estar entre 1 e 12.");
            RuleFor(x => x.ano)
                .NotEmpty().WithMessage("O ano é obrigatório.")
                .InclusiveBetween(1900, DateTime.Now.Year).WithMessage("O ano deve ser entre 1900 e o ano atual.");
        }
    }
}
