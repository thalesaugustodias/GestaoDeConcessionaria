using GestaoDeConcessionaria.Domain.Entities;

namespace GestaoDeConcessionaria.Application.Factories
{
    public static class ClienteFactory
    {
        public static Cliente Criar(string nome, string cpf, string telefone)
        {
            return new Cliente(nome, cpf, telefone);
        }
    }
}
