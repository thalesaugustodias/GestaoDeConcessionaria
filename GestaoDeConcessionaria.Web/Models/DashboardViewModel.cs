namespace GestaoDeConcessionaria.Web.Models
{
    public class DashboardViewModel
    {
        public int TotalVendas { get; set; }
        public decimal Faturamento { get; set; }
        public int TotalVeiculos { get; set; }
        public int TotalClientes { get; set; }
        public List<ChartDataItem> VendasMensais { get; set; } = new();
        public List<ChartDataItem> VendasPorFabricante { get; set; } = new();
        public List<ChartDataItem> VendasPorTipo { get; set; } = new();
        public List<ChartDataItem> DesempenhoConcessionarias { get; set; } = new();
    }

    public class ChartDataItem
    {
        public string Label { get; set; }
        public int Value { get; set; }
    }
}
