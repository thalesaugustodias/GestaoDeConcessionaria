namespace GestaoDeConcessionaria.Application.DTOs
{
    public record VendaDto(int VeiculoId, int ConcessionariaId, int ClienteId, DateTime DataVenda, decimal PrecoVenda);
}
