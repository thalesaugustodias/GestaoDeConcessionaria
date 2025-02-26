using GestaoDeConcessionaria.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace GestaoDeConcessionaria.Web.Models
{
    public class VeiculoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O modelo é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O modelo não pode ter mais de 100 caracteres.")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "O ano de fabricação é obrigatório.")]
        public int? AnoFabricacao { get; set; }

        [Required(ErrorMessage = "O preço é obrigatório.")]
        public decimal? Preco { get; set; }

        [Required(ErrorMessage = "O tipo de veículo é obrigatório.")]
        public TipoVeiculo? Tipo { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [MaxLength(500, ErrorMessage = "A descrição não pode exceder 500 caracteres.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O fabricante é obrigatório.")]
        public int FabricanteId { get; set; }
    }
}