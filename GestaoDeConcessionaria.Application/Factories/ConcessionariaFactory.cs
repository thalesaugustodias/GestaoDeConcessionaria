using GestaoDeConcessionaria.Application.DTOs;
using GestaoDeConcessionaria.Domain.Entities;

namespace GestaoDeConcessionaria.Application.Factories
{
    public static class ConcessionariaFactory
    {
        public static Concessionaria Criar(ConcessionariaDto dto)
        {
            return new Concessionaria(dto.Nome, dto.Rua, dto.Cidade, dto.Estado, dto.CEP, dto.Telefone, dto.Email, dto.CapacidadeMaximaVeiculos);
        }

        public static void Atualizar(Concessionaria entidade, ConcessionariaDto dto)
        {
            entidade.Atualizar(dto.Nome, dto.Rua, dto.Cidade, dto.Estado, dto.CEP, dto.Telefone, dto.Email, dto.CapacidadeMaximaVeiculos);
        }

        public static List<ConcessionariaDto> CriacaoDeConcessionariaDto(IEnumerable<Concessionaria> concessionarias)
        {
            return concessionarias.Select(c => new ConcessionariaDto(c.Id, c.Nome, c.Rua, c.Cidade, c.Estado, c.CEP, c.Telefone, c.Email, c.CapacidadeMaximaVeiculos)).ToList();
        }
    }
}
