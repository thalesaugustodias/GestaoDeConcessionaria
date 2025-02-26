namespace GestaoDeConcessionaria.Application.DTOs
{
    public record DashboardDto(int TotalVendas, decimal Faturamento, IEnumerable<DataPoint> VendasPorTipo, IEnumerable<DataPoint> VendasPorFabricante, IEnumerable<DataPoint> DesempenhoConcessionarias, List<DataPoint> VendasMensais, int TotalVeiculos, int TotalClientes);

    public record DataPoint(string Label, int Value);
}
