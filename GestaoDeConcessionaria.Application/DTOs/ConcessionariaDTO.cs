namespace GestaoDeConcessionaria.Application.DTOs
{
    public class ConcessionariaDTO
    {
        public string Nome { get; set; }
        public string Rua { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string CEP { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public int CapacidadeMaximaVeiculos { get; set; }
    }
}
