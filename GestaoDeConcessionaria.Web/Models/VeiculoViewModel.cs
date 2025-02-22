namespace GestaoDeConcessionaria.Web.Models
{
    public class VeiculoViewModel
    {
        public int Id { get; set; }
        public string Modelo { get; set; }
        public int AnoFabricacao { get; set; }
        public decimal Preco { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set; }
        public int FabricanteId { get; set; }
    }
}
