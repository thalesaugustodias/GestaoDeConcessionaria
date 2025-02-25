using GestaoDeConcessionaria.Application.Interfaces;

namespace GestaoDeConcessionaria.Application.Services
{
    public class ViaCepService : IViaCepService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ViaCepService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> ObterEnderecoPorCEPAsync(string cep)
        {
            var client = _httpClientFactory.CreateClient();
            if (client.DefaultRequestHeaders.Contains("RequestVerificationToken"))
                client.DefaultRequestHeaders.Remove("RequestVerificationToken");

            var response = await client.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            throw new Exception("CEP inválido ou API de CEP indisponível.");
        }
    }
}
