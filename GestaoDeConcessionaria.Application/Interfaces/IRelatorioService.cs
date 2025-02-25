using GestaoDeConcessionaria.Application.DTOs;

namespace GestaoDeConcessionaria.Application.Interfaces
{
    public interface IRelatorioService
    {
        Task<DashboardDTO> GerarRelatorioMensalAsync(int mes, int ano);
    }
}
