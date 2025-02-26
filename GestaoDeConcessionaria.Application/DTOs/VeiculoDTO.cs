namespace GestaoDeConcessionaria.Application.DTOs
{
    public record VeiculoDto(string Modelo, int AnoFabricacao, decimal Preco, string Tipo, string Descricao, int FabricanteId);
}
