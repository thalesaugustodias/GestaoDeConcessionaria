using GestaoDeConcessionaria.Application.Common.Events;
using GestaoDeConcessionaria.Application.Common.Interfaces;
using GestaoDeConcessionaria.Application.DTOs;
using GestaoDeConcessionaria.Application.Factories;
using GestaoDeConcessionaria.Application.Interfaces;
using MediatR;

namespace GestaoDeConcessionaria.Application.CQRS.Commands.Veiculos
{
    public class CriarVeiculosHandler(IVeiculoService v, IFabricanteService f, IRabbitMqPublisher publisher) : IRequestHandler<CriarVeiculosComando, VeiculoDto>
    {
        private readonly IVeiculoService _vS = v;
        private readonly IFabricanteService _fS = f;
        private readonly IRabbitMqPublisher _publisher = publisher;

        public async Task<VeiculoDto> Handle(CriarVeiculosComando cmd, CancellationToken ct)
        {
            var dto = cmd.Dto;
            var fab = await _fS.ObterPorIdAsync(dto.FabricanteId)
                ?? throw new KeyNotFoundException("Fabricante não encontrado");
            var novoVeiculo = VeiculoFactory.CriarVeiculo(dto, fab);
            await _vS.AdicionarAsync(novoVeiculo);
            //ajustar a implementação do publisher para regra de negócio correta posteriormente.
            //_publisher.Publish(
            //  new AlteracaoDeInventarioDeVeiculoEvento
            //  {
            //      VeiculoId = novoVeiculo.Id,
            //      ConcessionariaId = novoVeiculo.ConcessionariaId,
            //      NovoEstoque = 1
            //  },
            //  routingKey: "Estoque.alteracao"
            //);
            return VeiculoFactory.Create(novoVeiculo);
        }
    }
}
