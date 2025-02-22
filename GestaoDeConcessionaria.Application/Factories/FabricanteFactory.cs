using GestaoDeConcessionaria.Application.DTOs;
using GestaoDeConcessionaria.Domain.Entities;

namespace GestaoDeConcessionaria.Application.Factories
{
    public static class FabricanteFactory
    {
        public static Fabricante Criar(FabricanteDTO dto)
        {
            return new Fabricante(dto.Nome, dto.PaisOrigem, dto.AnoFundacao, dto.Website);
        }

        public static void Atualizar(Fabricante entidade, FabricanteDTO dto)
        {
            entidade.Atualizar(dto.Nome, dto.PaisOrigem, dto.AnoFundacao, dto.Website);
        }
    }
}
