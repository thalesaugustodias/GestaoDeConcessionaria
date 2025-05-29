using GestaoDeConcessionaria.Application.Interfaces;
using MediatR;

namespace GestaoDeConcessionaria.Application.Commands.Fabricantes
{
    public class DeletarFabricanteHandler(IFabricanteService svc) : IRequestHandler<DeletarFabricanteComando, Unit>
    {
        private readonly IFabricanteService _svc = svc;

        public async Task<Unit> Handle(DeletarFabricanteComando cmd, CancellationToken ct)
        {
            await _svc.DeletarAsync(cmd.Id);
            return Unit.Value;
        }
    }
}
