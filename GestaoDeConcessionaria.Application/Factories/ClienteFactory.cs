using GestaoDeConcessionaria.Application.DTOs;
using GestaoDeConcessionaria.Application.Extensions;
using GestaoDeConcessionaria.Domain.Entities;

namespace GestaoDeConcessionaria.Application.Factories
{
    public static class ClienteFactory
    {
        public static Cliente Criar(ClienteDto dto)
        {
            string cpf = dto.CPF.SomenteDigitos();
            return new Cliente(dto.Nome, cpf, dto.Telefone);
        }

        public static void Atualizar(Cliente entidade, ClienteDto dto)
        {
            string cpf = dto.CPF.SomenteDigitos();
            entidade.Atualizar(dto.Nome, cpf, dto.Telefone);
        }

        public static List<ClienteDto> CriarClienteDto(IEnumerable<Cliente> clientes)
        {
            return clientes.Select(c => new ClienteDto(c.Id, c.Nome, c.CPF, c.Telefone)).ToList();
        }
    }
}
