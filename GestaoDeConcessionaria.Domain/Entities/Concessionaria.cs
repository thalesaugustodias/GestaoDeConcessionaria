﻿using GestaoDeConcessionaria.Domain.Exceptions;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace GestaoDeConcessionaria.Domain.Entities
{
    public class Concessionaria
    {
        public Concessionaria() { }

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

        [JsonInclude]
        public int Id { get; private set; }
        [JsonInclude]
        public string Nome { get; private set; }
        [JsonInclude]
        public string Rua { get; private set; }
        [JsonInclude]
        public string Cidade { get; private set; }
        [JsonInclude]
        public string Estado { get; private set; }
        [JsonInclude]
        public string CEP { get; private set; }
        [JsonInclude]
        public string Telefone { get; private set; }
        [JsonInclude]
        public string Email { get; private set; }
        [JsonInclude]
        public int CapacidadeMaximaVeiculos { get; private set; }
        [JsonInclude]
        public bool Ativo { get; private set; }

        private static void Validar(string nome, string rua, string cidade, string estado, string cep, string telefone, string email, int capacidadeMaxima)
        {
            if (string.IsNullOrWhiteSpace(nome) || nome.Length > 100)
                throw new DomainValidationException("Nome da concessionária inválido.");
            if (string.IsNullOrWhiteSpace(rua) || string.IsNullOrWhiteSpace(cidade) || string.IsNullOrWhiteSpace(estado))
                throw new DomainValidationException("Endereço incompleto.");
            if (!Regex.IsMatch(cep, @"^\d{5}-?\d{3}$"))
                throw new DomainValidationException("CEP inválido.");
            if (!Regex.IsMatch(telefone, @"^\(?\d{2}\)?\s?\d{4,5}-?\d{4}$"))
                throw new DomainValidationException("Telefone inválido.");
            if (!Regex.IsMatch(email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
                throw new DomainValidationException("Email inválido.");
            if (capacidadeMaxima < 1)
                throw new DomainValidationException("Capacidade máxima deve ser um número positivo.");
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
