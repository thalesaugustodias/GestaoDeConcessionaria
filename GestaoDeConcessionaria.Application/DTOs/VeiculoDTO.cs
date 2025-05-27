using GestaoDeConcessionaria.Domain.Enums;

namespace GestaoDeConcessionaria.Application.DTOs
{
    public class VeiculoDto
    {
        public int Id { get; set; }
        public string Modelo { get; set; } = default!;
        public int AnoFabricacao { get; set; }
        public decimal Preco { get; set; }
        public string Tipo { get; set; } = default!;
        public string Descricao { get; set; } = default!;
        public int FabricanteId { get; set; }
        public string NomeFabricante { get; set; } = default!;
        public bool Ativo { get; set; }
    }
}
