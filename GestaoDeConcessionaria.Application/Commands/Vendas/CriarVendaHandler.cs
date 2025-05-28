using GestaoDeConcessionaria.Application.DTOs;
using GestaoDeConcessionaria.Application.Factories;
using GestaoDeConcessionaria.Application.Interfaces;
using MediatR;

namespace GestaoDeConcessionaria.Application.Commands.Vendas
{
    public class CriarVendaHandler(
      IVendaService v, IVeiculoService ve, IConcessionariaService c, IClienteService cl) : IRequestHandler<CriarVendasComando, VendaDetalhesDto>
    {
        private readonly IVendaService _vS = v;
        private readonly IVeiculoService _veS = ve;
        private readonly IConcessionariaService _cS = c;
        private readonly IClienteService _clS = cl;

        public async Task<VendaDetalhesDto> Handle(CriarVendasComando cmd, CancellationToken ct)
        {
            var dto = cmd.Dto;
            var v = await _veS.ObterPorIdAsync(dto.VeiculoId);
            var c = await _cS.ObterPorIdAsync(dto.ConcessionariaId);
            var cl = await _clS.ObterPorIdAsync(dto.ClienteId);
            var ent = VendaFactory.Criar(dto, v, c, cl);
            await _vS.AdicionarAsync(ent);
            return VendaFactory.CriarDetalhes(ent);
        }
    }
}
