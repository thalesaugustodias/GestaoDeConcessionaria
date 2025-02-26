namespace GestaoDeConcessionaria.Application.DTOs
{
    public record ConcessionariaDto(int Id, string Nome, string Rua, string Cidade, string Estado, string CEP, string Telefone, string Email, int CapacidadeMaximaVeiculos);
}
