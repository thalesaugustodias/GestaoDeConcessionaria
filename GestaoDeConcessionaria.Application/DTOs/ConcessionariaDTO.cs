namespace GestaoDeConcessionaria.Application.DTOs
{
    public record ConcessionariaDto(string Nome, string Rua, string Cidade, string Estado, string CEP, string Telefone, string Email, int CapacidadeMaximaVeiculos);
}
