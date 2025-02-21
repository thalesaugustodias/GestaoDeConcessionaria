using System.Text.RegularExpressions;

namespace GestaoDeConcessionaria.Domain.Entities
{
    public class Concessionaria
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Rua { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public string CEP { get; private set; }
        public string Telefone { get; private set; }
        public string Email { get; private set; }
        public int CapacidadeMaximaVeiculos { get; private set; }
        public bool Ativo { get; private set; }

        private Concessionaria() { }

        public Concessionaria(string nome, string rua, string cidade, string estado, string cep, string telefone, string email, int capacidadeMaxima)
        {
            Validar(nome, rua, cidade, estado, cep, telefone, email, capacidadeMaxima);
            Nome = nome;
            Rua = rua;
            Cidade = cidade;
            Estado = estado;
            CEP = cep;
            Telefone = telefone;
            Email = email;
            CapacidadeMaximaVeiculos = capacidadeMaxima;
            Ativo = true;
        }

        private static void Validar(string nome, string rua, string cidade, string estado, string cep, string telefone, string email, int capacidadeMaxima)
        {
            if (string.IsNullOrWhiteSpace(nome) || nome.Length > 100)
                throw new ArgumentException("Nome da concessionária inválido.");
            if (string.IsNullOrWhiteSpace(rua) || string.IsNullOrWhiteSpace(cidade) || string.IsNullOrWhiteSpace(estado))
                throw new ArgumentException("Endereço incompleto.");
            if (!Regex.IsMatch(cep, @"^\d{5}-?\d{3}$"))
                throw new ArgumentException("CEP inválido.");
            if (!Regex.IsMatch(telefone, @"^\(?\d{2}\)?\s?\d{4,5}-?\d{4}$"))
                throw new ArgumentException("Telefone inválido.");
            if (!Regex.IsMatch(email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
                throw new ArgumentException("Email inválido.");
            if (capacidadeMaxima < 1)
                throw new ArgumentException("Capacidade máxima deve ser um número positivo.");
        }

        public void Atualizar(string nome, string rua, string cidade, string estado, string cep, string telefone, string email, int capacidadeMaxima)
        {
            Validar(nome, rua, cidade, estado, cep, telefone, email, capacidadeMaxima);
            Nome = nome;
            Rua = rua;
            Cidade = cidade;
            Estado = estado;
            CEP = cep;
            Telefone = telefone;
            Email = email;
            CapacidadeMaximaVeiculos = capacidadeMaxima;
        }

        public void Deletar() => Ativo = false;

    }
}
