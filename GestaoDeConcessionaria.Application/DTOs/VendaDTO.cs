namespace GestaoDeConcessionaria.Application.DTOs
{
    public class VendaDTO
    {
        public int VeiculoId { get; set; }
        public int ConcessionariaId { get; set; }
        public int ClienteId { get; set; }
        public DateTime DataVenda { get; set; }
        public decimal PrecoVenda { get; set; }
    }
}
