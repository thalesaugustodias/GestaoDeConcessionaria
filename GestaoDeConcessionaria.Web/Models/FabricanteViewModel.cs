using System.ComponentModel.DataAnnotations;

namespace GestaoDeConcessionaria.Web.Models
{
    public class FabricanteViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O nome não pode ter mais de 100 caracteres.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O país de origem é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O país de origem não pode ter mais de 50 caracteres.")]
        public string? PaisOrigem { get; set; }

        [Required(ErrorMessage = "O ano de fundação é obrigatório.")]
        [Range(1800, 2100, ErrorMessage = "O ano de fundação deve ser entre 1800 e 2100.")]
        public int? AnoFundacao { get; set; }

        [Required(ErrorMessage = "O website é obrigatório.")]
        [Url(ErrorMessage = "O website deve ser uma URL válida.")]
        public string? Website { get; set; }
    }
}
