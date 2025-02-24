using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace GestaoDeConcessionaria.Domain.Entities
{
    public class Cliente
    {
        public Cliente() { }

        public Cliente(string nome, string cpf, string telefone)
        {
            Validar(nome, cpf, telefone);
            Nome = nome;
            CPF = cpf;
            Telefone = telefone;
            Ativo = true;
        }

        [JsonInclude]
        public int Id { get; private set; }
        [JsonInclude]
        public string Nome { get; private set; }
        [JsonInclude]
        public string CPF { get; private set; }
        [JsonInclude]
        public string Telefone { get; private set; }
        [JsonInclude]
        public bool Ativo { get; private set; }

        private static void Validar(string nome, string cpf, string telefone)
        {
            if (string.IsNullOrWhiteSpace(nome) || nome.Length > 100)
                throw new ArgumentException("Nome do cliente inválido.");
            if (!Regex.IsMatch(cpf, @"^\d{11}$"))
                throw new ArgumentException("CPF inválido.");
            if (string.IsNullOrWhiteSpace(telefone))
                throw new ArgumentException("Telefone inválido.");
        }

        public void Atualizar(string nome, string cpf, string telefone)
        {
            Validar(nome, cpf, telefone);
            Nome = nome;
            CPF = cpf;
            Telefone = telefone;
        }

        public void Deletar() => Ativo = false;
    }
}
