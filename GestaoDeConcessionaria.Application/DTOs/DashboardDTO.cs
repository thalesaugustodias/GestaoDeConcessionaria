namespace GestaoDeConcessionaria.Application.DTOs
{
    public class DashboardDTO
    {
        public int TotalVendas { get; set; }
        public decimal Faturamento { get; set; }
        public IEnumerable<DataPoint> VendasPorTipo { get; set; }
        public IEnumerable<DataPoint> VendasPorFabricante { get; set; }
        public IEnumerable<DataPoint> DesempenhoConcessionarias { get; set; }
        public List<DataPoint> VendasMensais { get; set; }
        public int TotalVeiculos { get; set; }
        public int TotalClientes { get; set; }
    }
    public class DataPoint
    {
        public string Label { get; set; }
        public int Value { get; set; }
    }
}
