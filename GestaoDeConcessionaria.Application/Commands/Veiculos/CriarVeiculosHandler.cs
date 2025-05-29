using GestaoDeConcessionaria.Application.DTOs;
using GestaoDeConcessionaria.Application.Factories;
using GestaoDeConcessionaria.Application.Interfaces;
using MediatR;

namespace GestaoDeConcessionaria.Application.Commands.Veiculos
{
    public class CriarVeiculosHandler(IVeiculoService v, IFabricanteService f) : IRequestHandler<CriarVeiculosComando, VeiculoDto>
    {
        private readonly IVeiculoService _vS = v;
        private readonly IFabricanteService _fS = f;

        public async Task<VeiculoDto> Handle(CriarVeiculosComando cmd, CancellationToken ct)
        {
            var dto = cmd.Dto;
            var fab = await _fS.ObterPorIdAsync(dto.FabricanteId)
                ?? throw new KeyNotFoundException("Fabricante não encontrado");
            var ent = VeiculoFactory.CriarVeiculo(dto, fab);
            await _vS.AdicionarAsync(ent);
            return VeiculoFactory.Create(ent);
        }
    }
}
