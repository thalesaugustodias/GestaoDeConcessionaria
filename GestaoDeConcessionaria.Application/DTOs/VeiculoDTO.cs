namespace GestaoDeConcessionaria.Application.DTOs
{
    public class VeiculoDTO
    {
        public string Modelo { get; set; }
        public int AnoFabricacao { get; set; }
        public decimal Preco { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set; }
        public int FabricanteId { get; set; }
    }
}
