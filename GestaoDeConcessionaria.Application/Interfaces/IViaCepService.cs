namespace GestaoDeConcessionaria.Application.Interfaces
{
    public interface IViaCepService
    {
        Task<string> ObterEnderecoPorCEPAsync(string cep);
    }
}
