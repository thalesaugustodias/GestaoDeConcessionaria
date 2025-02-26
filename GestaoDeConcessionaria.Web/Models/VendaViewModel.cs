using System.ComponentModel.DataAnnotations;

namespace GestaoDeConcessionaria.Web.Models
{
    public class VendaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Selecione um veículo.")]
        public int VeiculoId { get; set; }

        [Required(ErrorMessage = "O modelo é obrigatório.")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "Selecione uma concessionária.")]
        public int ConcessionariaId { get; set; }

        public string ConcessionariaNome { get; set; }

        [Required(ErrorMessage = "Selecione um cliente.")]
        public int ClienteId { get; set; }

        public string ClienteNome { get; set; }

        [Required(ErrorMessage = "A data da venda é obrigatória.")]
        public DateTime DataVenda { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "O preço de venda é obrigatório.")]
        [Range(0, double.MaxValue, ErrorMessage = "O preço deve ser maior ou igual a zero.")]
        public decimal PrecoVenda { get; set; }

        [Required(ErrorMessage = "O protocolo de venda é obrigatório.")]
        public string ProtocoloVenda { get; set; }
    }
}
