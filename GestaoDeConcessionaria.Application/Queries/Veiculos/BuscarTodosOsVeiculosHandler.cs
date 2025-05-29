using GestaoDeConcessionaria.Application.DTOs;
using GestaoDeConcessionaria.Application.Factories;
using GestaoDeConcessionaria.Application.Interfaces;
using MediatR;

namespace GestaoDeConcessionaria.Application.Queries.Veiculos
{
    public class BuscarTodosOsVeiculosHandler : IRequestHandler<BuscarTodosOsVeiculosQuery, IEnumerable<VeiculoDto>>
    {
        private readonly IVeiculoService _svc;
        public BuscarTodosOsVeiculosHandler(IVeiculoService svc) => _svc = svc;

        public async Task<IEnumerable<VeiculoDto>> Handle(BuscarTodosOsVeiculosQuery q, CancellationToken ct)
        {
            var list = await _svc.ObterTodosAsync();
            return list.Select(v => VeiculoFactory.Create(v));
        }
    }
}
