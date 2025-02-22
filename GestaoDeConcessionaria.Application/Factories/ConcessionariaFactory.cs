using GestaoDeConcessionaria.Application.DTOs;
using GestaoDeConcessionaria.Domain.Entities;

namespace GestaoDeConcessionaria.Application.Factories
{
    public static class ConcessionariaFactory
    {
        public static Concessionaria Criar(ConcessionariaDTO dto)
        {
            return new Concessionaria(dto.Nome, dto.Rua, dto.Cidade, dto.Estado, dto.CEP, dto.Telefone, dto.Email, dto.CapacidadeMaximaVeiculos);
        }

        public static void Atualizar(Concessionaria entidade, ConcessionariaDTO dto)
        {
            entidade.Atualizar(dto.Nome, dto.Rua, dto.Cidade, dto.Estado, dto.CEP, dto.Telefone, dto.Email, dto.CapacidadeMaximaVeiculos);
        }
    }
}
