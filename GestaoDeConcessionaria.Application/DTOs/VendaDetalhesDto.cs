namespace GestaoDeConcessionaria.Application.DTOs
{
    public class VendaDetalhesDto
    {
        public int Id { get; set; }
        public int VeiculoId { get; set; }
        public string VeiculoModelo { get; set; } = "";
        public int ConcessionariaId { get; set; }
        public string ConcessionariaNome { get; set; } = "";
        public int ClienteId { get; set; }
        public string ClienteNome { get; set; } = "";
        public DateTime DataVenda { get; set; }
        public decimal PrecoVenda { get; set; }
        public string ProtocoloVenda { get; set; } = "";
    }
}
