namespace GestaoDeConcessionaria.Application.DTOs
{
    public record VendaDadosDeCriacaoDto(IEnumerable<VeiculoDto> Veiculos, IEnumerable<ConcessionariaDto> Concessionarias, IEnumerable<ClienteDto> Clientes);
}
