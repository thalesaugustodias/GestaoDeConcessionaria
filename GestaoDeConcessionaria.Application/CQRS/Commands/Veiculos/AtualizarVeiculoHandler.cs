using GestaoDeConcessionaria.Application.Factories;
using GestaoDeConcessionaria.Application.Interfaces;
using MediatR;

namespace GestaoDeConcessionaria.Application.CQRS.Commands.Veiculos
{
    public class AtualizarVeiculoHandler(IVeiculoService v, IFabricanteService f) : IRequestHandler<AtualizarVeiculoComando, Unit>
    {
        private readonly IVeiculoService _vS = v;
        private readonly IFabricanteService _fS = f;

        public async Task<Unit> Handle(AtualizarVeiculoComando cmd, CancellationToken ct)
        {
            var ent = await _vS.ObterPorIdAsync(cmd.Dto.Id)
                ?? throw new KeyNotFoundException("Veículo não encontrado");
            var fab = await _fS.ObterPorIdAsync(cmd.Dto.FabricanteId)
                ?? throw new KeyNotFoundException("Fabricante não encontrado");
            VeiculoFactory.Atualizar(ent, cmd.Dto, fab);
            await _vS.AtualizarAsync(ent);
            return Unit.Value;
        }
    }
}
