using GestaoDeConcessionaria.Application.DTOs;
using GestaoDeConcessionaria.Domain.Entities;
using GestaoDeConcessionaria.Domain.Enums;

namespace GestaoDeConcessionaria.Application.Factories
{
    public static class VeiculoFactory
    {
        public static VeiculoDto Create(Veiculo v) =>
            new()
            {
                Id = v.Id,
                Modelo = v.Modelo,
                AnoFabricacao = v.AnoFabricacao,
                Preco = v.Preco,
                Tipo = v.Tipo.ToString(),
                Descricao = v.Descricao,
                FabricanteId = v.FabricanteId,
                NomeFabricante = v.Fabricante?.Nome ?? string.Empty,
                Ativo = v.Ativo
            };

        public static Veiculo CriarVeiculo(VeiculoDto dto, Fabricante fabricante)
        {
            if (!Enum.TryParse<TipoVeiculo>(dto.Tipo, true, out var tipoEnum))
                throw new ArgumentException($"TipoVeiculo inválido: {dto.Tipo}");

            return new Veiculo(
                modelo: dto.Modelo,
                anoFabricacao: dto.AnoFabricacao,
                preco: dto.Preco,
                tipo: tipoEnum,
                descricao: dto.Descricao,
                fabricante: fabricante
            );
        }

        public static void Atualizar(Veiculo entidade, VeiculoDto dto, Fabricante fabricante)
        {
            if (!Enum.TryParse<TipoVeiculo>(dto.Tipo, true, out var tipoEnum))
                throw new ArgumentException($"TipoVeiculo inválido: {dto.Tipo}");

            entidade.Atualizar(
                modelo: dto.Modelo,
                anoFabricacao: dto.AnoFabricacao,
                preco: dto.Preco,
                tipo: tipoEnum,
                descricao: dto.Descricao,
                fabricante: fabricante
            );
        }

        public static IEnumerable<VeiculoDto> CreateList(IEnumerable<Veiculo> veiculos) =>
            veiculos.Select(Create);
    }

}
