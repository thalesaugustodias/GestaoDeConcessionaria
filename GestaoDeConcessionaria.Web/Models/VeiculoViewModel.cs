using GestaoDeConcessionaria.Domain.Enums;

namespace GestaoDeConcessionaria.Web.Models
{
    public class VeiculoViewModel
    {
        public int Id { get; set; }
        public string Modelo { get; set; }
        public int AnoFabricacao { get; set; }
        public decimal Preco { get; set; }
        public TipoVeiculo Tipo { get; set; }
        public string Descricao { get; set; }
        public int FabricanteId { get; set; }
        public string NomeCompleto => $"{Modelo} ({AnoFabricacao})";
    }
}
