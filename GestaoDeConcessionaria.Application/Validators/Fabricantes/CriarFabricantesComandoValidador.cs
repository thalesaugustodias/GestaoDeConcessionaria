using FluentValidation;
using GestaoDeConcessionaria.Application.CQRS.Commands.Fabricantes;

namespace GestaoDeConcessionaria.Application.Validators.Fabricantes
{
    public class CriarFabricantesComandoValidador : AbstractValidator<CriarFabricantesComando>
    {
        public CriarFabricantesComandoValidador()
        {
            RuleFor(x => x.Dto.Nome).NotEmpty();
            RuleFor(x => x.Dto.PaisOrigem).NotEmpty();
            RuleFor(x => x.Dto.AnoFundacao)
                .InclusiveBetween(1800, DateTime.Now.Year);
            RuleFor(x => x.Dto.Website).NotEmpty().Must(u => Uri.IsWellFormedUriString(u, UriKind.Absolute));
        }
    }
}
