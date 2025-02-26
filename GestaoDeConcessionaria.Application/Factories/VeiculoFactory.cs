using GestaoDeConcessionaria.Application.DTOs;
using GestaoDeConcessionaria.Domain.Entities;
using GestaoDeConcessionaria.Domain.Enums;

namespace GestaoDeConcessionaria.Application.Factories
{
    public static class VeiculoFactory
    {
        public static Veiculo CriarVeiculo(VeiculoDto dto, Fabricante fabricante)
        {
            TipoVeiculo tipo = dto.Tipo;
            return new Veiculo(dto.Modelo, dto.AnoFabricacao, dto.Preco, tipo, dto.Descricao, fabricante);
        }

        public static void Atualizar(Veiculo entidade, VeiculoDto dto, Fabricante fabricante)
        {
            entidade.Atualizar(dto.Modelo, dto.AnoFabricacao, dto.Preco, dto.Tipo, dto.Descricao, fabricante);
        }

        public static List<VeiculoDto> CriacaoDeVeiculoDto(IEnumerable<Veiculo> veiculos)
        {
            return veiculos.Select(v => new VeiculoDto(v.Id, v.Modelo, v.AnoFabricacao, v.Preco, v.Tipo, v.Descricao, v.FabricanteId)).ToList();
        }
    }
}
