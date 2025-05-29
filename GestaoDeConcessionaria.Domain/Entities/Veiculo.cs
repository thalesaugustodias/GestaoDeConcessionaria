using GestaoDeConcessionaria.Domain.Enums;
using GestaoDeConcessionaria.Domain.Exceptions;
using System.Text.Json.Serialization;

namespace GestaoDeConcessionaria.Domain.Entities
{
    public class Veiculo
    {
        public Veiculo() { }

        public Veiculo(string modelo, int anoFabricacao, decimal preco, TipoVeiculo tipo, string descricao, Fabricante fabricante)
        {
            Validar(modelo, anoFabricacao, preco, fabricante);
            Modelo = modelo;
            AnoFabricacao = anoFabricacao;
            Preco = preco;
            Tipo = tipo;
            Descricao = descricao?.Length <= 500 ? descricao : throw new ArgumentException("Descrição excede 500 caracteres.");
            Fabricante = fabricante;
            FabricanteId = fabricante.Id;
            Ativo = true;
        }

        [JsonInclude]
        public int Id { get; private set; }
        [JsonInclude]
        public string Modelo { get; private set; }
        [JsonInclude]
        public int AnoFabricacao { get; private set; }
        [JsonInclude]
        public decimal Preco { get; private set; }
        [JsonInclude]
        public TipoVeiculo Tipo { get; private set; }
        [JsonInclude]
        public string Descricao { get; private set; }
        [JsonInclude]
        public int FabricanteId { get; private set; }
        [JsonInclude]
        public Fabricante Fabricante { get; private set; }
        [JsonInclude]
        public bool Ativo { get; private set; }

        private static void Validar(string modelo, int anoFabricacao, decimal preco, Fabricante fabricante)
        {
            if (string.IsNullOrWhiteSpace(modelo) || modelo.Length > 100)
                throw new DomainValidationException("Modelo do veículo inválido.");
            if (anoFabricacao > DateTime.Now.Year)
                throw new DomainValidationException("Ano de fabricação não pode ser no futuro.");
            if (preco <= 0)
                throw new DomainValidationException("Preço deve ser um valor positivo.");
            if (fabricante == null)
                throw new DomainValidationException("Fabricante é obrigatório.");
        }

        public void Atualizar(string modelo, int anoFabricacao, decimal preco, TipoVeiculo tipo, string descricao, Fabricante fabricante)
        {
            Validar(modelo, anoFabricacao, preco, fabricante);
            Modelo = modelo;
            AnoFabricacao = anoFabricacao;
            Preco = preco;
            Tipo = tipo;
            Descricao = descricao?.Length <= 500 ? descricao : throw new DomainValidationException("Descrição excede 500 caracteres.");
            Fabricante = fabricante;
            FabricanteId = fabricante.Id;
        }

        public void Deletar() => Ativo = false;
    }
}
