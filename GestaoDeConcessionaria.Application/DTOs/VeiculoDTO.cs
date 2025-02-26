using GestaoDeConcessionaria.Domain.Enums;

namespace GestaoDeConcessionaria.Application.DTOs
{
    public record VeiculoDto(int Id, string Modelo, int AnoFabricacao, decimal Preco, TipoVeiculo Tipo, string Descricao, int FabricanteId);
}
