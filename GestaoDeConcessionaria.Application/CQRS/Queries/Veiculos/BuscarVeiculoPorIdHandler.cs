using GestaoDeConcessionaria.Application.DTOs;
using GestaoDeConcessionaria.Application.Factories;
using GestaoDeConcessionaria.Application.Interfaces;
using MediatR;

namespace GestaoDeConcessionaria.Application.CQRS.Queries.Veiculos
{
    public class BuscarVeiculoPorIdHandler : IRequestHandler<BuscarVeiculoPorIdQuery, VeiculoDto>
    {
        private readonly IVeiculoService _svc;
        public BuscarVeiculoPorIdHandler(IVeiculoService svc) => _svc = svc;

        public async Task<VeiculoDto> Handle(BuscarVeiculoPorIdQuery q, CancellationToken ct)
        {
            var v = await _svc.ObterPorIdAsync(q.Id)
                ?? throw new KeyNotFoundException("Veículo não encontrado");
            return VeiculoFactory.Create(v);
        }
    }
}
