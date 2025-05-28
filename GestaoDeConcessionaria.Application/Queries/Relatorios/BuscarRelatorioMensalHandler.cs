using GestaoDeConcessionaria.Application.DTOs;
using GestaoDeConcessionaria.Application.Interfaces;
using MediatR;

namespace GestaoDeConcessionaria.Application.Queries.Relatorios
{
    public class BuscarRelatorioMensalHandler(IRelatorioService svc) : IRequestHandler<BuscarRelatorioMensalQuery, DashboardDto>
    {
        private readonly IRelatorioService _svc = svc;

        public async Task<DashboardDto> Handle(BuscarRelatorioMensalQuery q, CancellationToken ct)
            => await _svc.GerarRelatorioMensalAsync(q.mes, q.ano);
    }
}
