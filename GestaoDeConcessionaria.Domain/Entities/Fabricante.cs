using GestaoDeConcessionaria.Domain.Exceptions;
using System;
using System.Text.Json.Serialization;

namespace GestaoDeConcessionaria.Domain.Entities
{
    public class Fabricante
    {
        public Fabricante() { }

        public Fabricante(string nome, string paisOrigem, int anoFundacao, string website)
        {
            Validar(nome, paisOrigem, anoFundacao, website);
            Nome = nome;
            PaisOrigem = paisOrigem;
            AnoFundacao = anoFundacao;
            Website = website;
            Ativo = true;
        }

        [JsonInclude]
        public int Id { get; private set; }

        [JsonInclude]
        public string Nome { get; private set; }

        [JsonInclude]
        public string PaisOrigem { get; private set; }

        [JsonInclude]
        public int AnoFundacao { get; private set; }

        [JsonInclude]
        public string Website { get; private set; }

        [JsonInclude]
        public bool Ativo { get; private set; }       

        private static void Validar(string nome, string paisOrigem, int anoFundacao, string website)
        {
            if (string.IsNullOrWhiteSpace(nome) || nome.Length > 100)
                throw new DomainValidationException("Nome do fabricante inválido.");
            if (string.IsNullOrWhiteSpace(paisOrigem) || paisOrigem.Length > 50)
                throw new DomainValidationException("País de origem inválido.");
            if (anoFundacao >= DateTime.Now.Year)
                throw new DomainValidationException("O ano de fundação deve ser menor que o ano atual.");
            if (!Uri.TryCreate(website, UriKind.Absolute, out _))
                throw new DomainValidationException("Website inválido.");
        }

        public void Atualizar(string nome, string paisOrigem, int anoFundacao, string website)
        {
            Validar(nome, paisOrigem, anoFundacao, website);
            Nome = nome;
            PaisOrigem = paisOrigem;
            AnoFundacao = anoFundacao;
            Website = website;
        }

        public void Deletar() => Ativo = false;
    }
}
