using GestaoDeConcessionaria.Application.DTOs;
using GestaoDeConcessionaria.Domain.Entities;

namespace GestaoDeConcessionaria.Application.Factories
{
    public static class FabricanteFactory
    {
        public static Fabricante Criar(FabricanteDto dto)
        {
            return new Fabricante(dto.Nome, dto.PaisOrigem, dto.AnoFundacao, dto.Website);
        }

        public static void Atualizar(Fabricante entidade, FabricanteDto dto)
        {
            entidade.Atualizar(dto.Nome, dto.PaisOrigem, dto.AnoFundacao, dto.Website);
        }
    }
}
