using GestaoDeConcessionaria.Application.Common.Events;
using GestaoDeConcessionaria.Application.Common.Interfaces;
using GestaoDeConcessionaria.Application.DTOs;
using GestaoDeConcessionaria.Application.Factories;
using GestaoDeConcessionaria.Application.Interfaces;
using MediatR;

namespace GestaoDeConcessionaria.Application.CQRS.Commands.Vendas
{
    public class CriarVendaHandler(
      IVendaService v, IVeiculoService ve, IConcessionariaService c, IClienteService cl, IRabbitMqPublisher publisher) : IRequestHandler<CriarVendasComando, VendaDetalhesDto>
    {
        private readonly IVendaService _vS = v;
        private readonly IVeiculoService _veS = ve;
        private readonly IConcessionariaService _cS = c;
        private readonly IClienteService _clS = cl;
        private readonly IRabbitMqPublisher _publisher = publisher;

        public async Task<VendaDetalhesDto> Handle(CriarVendasComando cmd, CancellationToken ct)
        {
            var dto = cmd.Dto;
            var v = await _veS.ObterPorIdAsync(dto.VeiculoId);
            var c = await _cS.ObterPorIdAsync(dto.ConcessionariaId);
            var cl = await _clS.ObterPorIdAsync(dto.ClienteId);
            var venda = VendaFactory.Criar(dto, v, c, cl);
            await _vS.AdicionarAsync(venda);

            _publisher.Publish(new VendaCriadaEvento
               {
                   VendaId = venda.Id,
                   VeiculoId = venda.VeiculoId,
                   ConcessionariaId = venda.ConcessionariaId,
                   ClienteId = venda.ClienteId,
                   DataVenda = venda.DataVenda,
                   PrecoVenda = venda.PrecoVenda
               },
                routingKey: "venda.criada"
            );
            return VendaFactory.CriarDetalhes(venda);
        }
    }
}
