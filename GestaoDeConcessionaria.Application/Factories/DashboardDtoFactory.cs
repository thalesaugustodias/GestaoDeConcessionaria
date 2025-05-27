using GestaoDeConcessionaria.Application.DTOs;

namespace GestaoDeConcessionaria.Application.Factories
{
    public static class DashboardDtoFactory
    {
        public static DashboardDto Criar(
            int totalVendas,
            decimal faturamento,
            List<DataPoint>? vendasPorTipo,
            List<DataPoint>? vendasPorFabricante,
            List<DataPoint>? desempenhoConcessionarias,
            List<DataPoint>? vendasPorDia,
            int totalVeiculosAtivos,
            int totalClientesAtivos)
        {
            return new DashboardDto(
                totalVendas,
                faturamento,
                vendasPorTipo ?? [],
                vendasPorFabricante ?? [],
                desempenhoConcessionarias ?? [],
                vendasPorDia ?? [],
                totalVeiculosAtivos,
                totalClientesAtivos
            );
        }
    }
}
