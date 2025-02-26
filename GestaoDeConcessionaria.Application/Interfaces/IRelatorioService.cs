using GestaoDeConcessionaria.Application.DTOs;

namespace GestaoDeConcessionaria.Application.Interfaces
{
    public interface IRelatorioService
    {
        Task<DashboardDto> GerarRelatorioMensalAsync(int mes, int ano);
    }
}
