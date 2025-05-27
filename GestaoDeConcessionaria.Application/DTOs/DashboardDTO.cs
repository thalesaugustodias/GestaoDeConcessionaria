namespace GestaoDeConcessionaria.Application.DTOs
{
    public record DashboardDto(int TotalVendas, decimal Faturamento, List<DataPoint> VendasPorTipo, List<DataPoint> VendasPorFabricante, List<DataPoint> DesempenhoConcessionarias, List<DataPoint> VendasMensais, int TotalVeiculos, int TotalClientes);

    public record DataPoint(string Label, int Value);
}
