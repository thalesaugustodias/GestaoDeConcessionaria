using System.Text.RegularExpressions;

namespace GestaoDeConcessionaria.Domain.Entities
{
    public class Cliente
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string CPF { get; private set; }
        public string Telefone { get; private set; }
        public bool Ativo { get; private set; }

        private Cliente() { }

        public Cliente(string nome, string cpf, string telefone)
        {
            Validar(nome, cpf, telefone);
            Nome = nome;
            CPF = cpf;
            Telefone = telefone;
            Ativo = true;
        }

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
