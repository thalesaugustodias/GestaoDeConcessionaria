using GestaoDeConcessionaria.Application.DTOs;
using GestaoDeConcessionaria.Domain.Entities;
using GestaoDeConcessionaria.Domain.Enums;

namespace GestaoDeConcessionaria.Application.Factories
{
    public static class VeiculoFactory
    {
        public static Veiculo Criar(VeiculoDTO dto, Fabricante fabricante)
        {
            if (!Enum.TryParse(dto.Tipo, true, out TipoVeiculo tipo))
                throw new ArgumentException("Tipo de veículo inválido.");

            return new Veiculo(dto.Modelo, dto.AnoFabricacao, dto.Preco, tipo, dto.Descricao, fabricante);
        }

        public static void Atualizar(Veiculo entidade, VeiculoDTO dto, Fabricante fabricante)
        {
            if (!Enum.TryParse(dto.Tipo, true, out TipoVeiculo tipo))
                throw new ArgumentException("Tipo de veículo inválido.");

            entidade.Atualizar(dto.Modelo, dto.AnoFabricacao, dto.Preco, tipo, dto.Descricao, fabricante);
        }
    }
}
