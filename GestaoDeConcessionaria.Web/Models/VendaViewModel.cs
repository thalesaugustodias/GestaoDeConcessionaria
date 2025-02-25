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
        public DateTime DataVenda { get; set; }
        public decimal PrecoVenda { get; set; }
    }
}
