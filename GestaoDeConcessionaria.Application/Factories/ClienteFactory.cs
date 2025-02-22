using GestaoDeConcessionaria.Application.DTOs;
using GestaoDeConcessionaria.Domain.Entities;

namespace GestaoDeConcessionaria.Application.Factories
{
    public static class ClienteFactory
    {
        public static Cliente Criar(ClienteDTO dto)
        {
            return new Cliente(dto.Nome, dto.CPF, dto.Telefone);
        }

        public static void Atualizar(Cliente entidade, ClienteDTO dto)
        {
            entidade.Atualizar(dto.Nome, dto.CPF, dto.Telefone);
        }
    }
}
