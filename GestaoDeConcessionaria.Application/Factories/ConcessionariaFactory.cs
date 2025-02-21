using GestaoDeConcessionaria.Domain.Entities;

namespace GestaoDeConcessionaria.Application.Factories
{
    public static class ConcessionariaFactory
    {
        public static Concessionaria Criar(string nome, string rua, string cidade, string estado, string cep, string telefone, string email, int capacidade)
        {
            return new Concessionaria(nome, rua, cidade, estado, cep, telefone, email, capacidade);
        }
    }
}
