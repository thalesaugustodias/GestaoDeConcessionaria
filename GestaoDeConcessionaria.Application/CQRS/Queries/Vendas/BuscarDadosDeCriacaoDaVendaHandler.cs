using GestaoDeConcessionaria.Application.DTOs;
using GestaoDeConcessionaria.Application.Factories;
using GestaoDeConcessionaria.Application.Interfaces;
using MediatR;

namespace GestaoDeConcessionaria.Application.CQRS.Queries.Vendas
{
    public class BuscarDadosDeCriacaoDaVendaHandler(IVeiculoService ve, IConcessionariaService co, IClienteService cl) : IRequestHandler<BuscarDadosDeCriacaoDaVendaQuery, VendaDadosDeCriacaoDto>
    {
        private readonly IVeiculoService _ve = ve;
        private readonly IConcessionariaService _co = co;
        private readonly IClienteService _cl = cl;

        public async Task<VendaDadosDeCriacaoDto> Handle(BuscarDadosDeCriacaoDaVendaQuery q, CancellationToken ct)
        {
            var vs = VeiculoFactory.CreateList(await _ve.ObterTodosAsync());
            var cs = ConcessionariaFactory.CriacaoDeConcessionariaDto(await _co.ObterTodosAsync());
            var cl = ClienteFactory.CriarClienteDto(await _cl.ObterTodosAsync());
            return new VendaDadosDeCriacaoDto(vs, cs, cl);
        }
    }
}
