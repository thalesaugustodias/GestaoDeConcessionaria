namespace GestaoDeConcessionaria.Application
{
    public class JwtSettings
    {
        public string Chave { get; set; } = string.Empty;
        public string Emissor { get; set; } = string.Empty;
        public string Publico { get; set; } = string.Empty;
        public int ExpiracaoHoras { get; set; }
    }
}
