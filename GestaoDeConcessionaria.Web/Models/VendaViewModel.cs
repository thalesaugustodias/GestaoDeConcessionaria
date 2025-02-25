using System.ComponentModel.DataAnnotations;

namespace GestaoDeConcessionaria.Web.Models
{
    public class VendaViewModel
    {
        public int Id { get; set; }
        public int VeiculoId { get; set; }
        public string Modelo { get; set; }
        public int ConcessionariaId { get; set; }
        public string ConcessionariaNome { get; set; }
        public int ClienteId { get; set; }
        public string ClienteNome { get; set; }
        public DateTime DataVenda { get; set; } = DateTime.Today;
        public decimal PrecoVenda { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "O preço deve ser maior ou igual a zero.")]
        public string ProtocoloVenda { get; set; }
    }
}
