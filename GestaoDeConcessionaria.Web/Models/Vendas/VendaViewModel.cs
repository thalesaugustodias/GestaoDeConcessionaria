using System.ComponentModel.DataAnnotations;

namespace GestaoDeConcessionaria.Web.Models.Vendas
{
    public class VendaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Selecione um veículo.")]
        public int? VeiculoId { get; set; }

        public string? Modelo { get; set; }

        [Required(ErrorMessage = "Selecione uma concessionária.")]
        public int? ConcessionariaId { get; set; }

        public string? ConcessionariaNome { get; set; }

        [Required(ErrorMessage = "Selecione um cliente.")]
        public int? ClienteId { get; set; }

        public string? ClienteNome { get; set; }

        [Required(ErrorMessage = "A data da venda é obrigatória.")]
        public DateTime DataVenda { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "O preço de venda é obrigatório.")]
        [Range(0, double.MaxValue, ErrorMessage = "O preço deve ser maior ou igual a zero.")]
        public decimal PrecoVenda { get; set; }
    }
}
