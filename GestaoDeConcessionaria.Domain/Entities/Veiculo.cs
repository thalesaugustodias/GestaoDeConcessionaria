using GestaoDeConcessionaria.Domain.Enums;

namespace GestaoDeConcessionaria.Domain.Entities
{
    public class Veiculo
    {
        public int Id { get; private set; }
        public string Modelo { get; private set; }
        public int AnoFabricacao { get; private set; }
        public decimal Preco { get; private set; }
        public TipoVeiculo Tipo { get; private set; }
        public string Descricao { get; private set; }
        public int FabricanteId { get; private set; }
        public Fabricante Fabricante { get; private set; }
        public bool Ativo { get; private set; }

        private Veiculo() { }

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

        private static void Validar(string modelo, int anoFabricacao, decimal preco, Fabricante fabricante)
        {
            if (string.IsNullOrWhiteSpace(modelo) || modelo.Length > 100)
                throw new ArgumentException("Modelo do veículo inválido.");
            if (anoFabricacao > DateTime.Now.Year)
                throw new ArgumentException("Ano de fabricação não pode ser no futuro.");
            if (preco <= 0)
                throw new ArgumentException("Preço deve ser um valor positivo.");
            if (fabricante == null)
                throw new ArgumentException("Fabricante é obrigatório.");
        }

        public void Atualizar(string modelo, int anoFabricacao, decimal preco, TipoVeiculo tipo, string descricao, Fabricante fabricante)
        {
            Validar(modelo, anoFabricacao, preco, fabricante);
            Modelo = modelo;
            AnoFabricacao = anoFabricacao;
            Preco = preco;
            Tipo = tipo;
            Descricao = descricao?.Length <= 500 ? descricao : throw new ArgumentException("Descrição excede 500 caracteres.");
            Fabricante = fabricante;
            FabricanteId = fabricante.Id;
        }

        public void Deletar() => Ativo = false;
    }
}
