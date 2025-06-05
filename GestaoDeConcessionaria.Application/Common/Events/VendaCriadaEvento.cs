namespace GestaoDeConcessionaria.Application.Common.Events
{
    partial class VendaCriadaEvento
    {
        public int VendaId { get; init; }
        public int VeiculoId { get; init; }
        public int ConcessionariaId { get; init; }
        public int ClienteId { get; init; }
        public DateTime DataVenda { get; init; }
        public decimal PrecoVenda { get; init; }
    }
}
