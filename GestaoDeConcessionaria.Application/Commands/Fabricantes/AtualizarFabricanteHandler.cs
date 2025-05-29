using GestaoDeConcessionaria.Application.Factories;
using GestaoDeConcessionaria.Application.Interfaces;
using MediatR;

namespace GestaoDeConcessionaria.Application.Commands.Fabricantes
{
    public class AtualizarFabricanteHandler(IFabricanteService svc) : IRequestHandler<AtualizarFabricanteComando, Unit>
    {
        private readonly IFabricanteService _svc = svc;

        public async Task<Unit> Handle(AtualizarFabricanteComando cmd, CancellationToken ct)
        {
            var ent = await _svc.ObterPorIdAsync(cmd.Id)
                ?? throw new KeyNotFoundException("Fabricante não encontrado");
            FabricanteFactory.Atualizar(ent, cmd.Dto);
            await _svc.AtualizarAsync(ent);
            return Unit.Value;
        }
    }
}
