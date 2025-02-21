namespace GestaoDeConcessionaria.Domain.Entities
{
    public class Fabricante
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string PaisOrigem { get; private set; }
        public int AnoFundacao { get; private set; }
        public string Website { get; private set; }
        public bool Ativo { get; private set; }

        private Fabricante() { }

        public Fabricante(string nome, string paisOrigem, int anoFundacao, string website)
        {
            Validar(nome, paisOrigem, anoFundacao, website);
            Nome = nome;
            PaisOrigem = paisOrigem;
            AnoFundacao = anoFundacao;
            Website = website;
            Ativo = true;
        }

        private static void Validar(string nome, string paisOrigem, int anoFundacao, string website)
        {
            if (string.IsNullOrWhiteSpace(nome) || nome.Length > 100)
                throw new ArgumentException("Nome do fabricante inválido.");
            if (string.IsNullOrWhiteSpace(paisOrigem) || paisOrigem.Length > 50)
                throw new ArgumentException("País de origem inválido.");
            if (anoFundacao >= DateTime.Now.Year)
                throw new ArgumentException("O ano de fundação deve ser menor que o ano atual.");
            if (!Uri.TryCreate(website, UriKind.Absolute, out _))
                throw new ArgumentException("Website inválido.");
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
