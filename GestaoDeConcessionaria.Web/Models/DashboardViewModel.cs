namespace GestaoDeConcessionaria.Web.Models
{
    public class DashboardViewModel
    {
        public int TotalVendas { get; set; }
        public decimal Faturamento { get; set; }
        public int TotalVeiculos { get; set; }
        public int TotalClientes { get; set; }
        public List<ChartData> VendasMensais { get; set; }
        public List<ChartData> VendasPorFabricante { get; set; }
    }

    public class ChartData
    {
        public string Label { get; set; }
        public int Value { get; set; }
    }
}
