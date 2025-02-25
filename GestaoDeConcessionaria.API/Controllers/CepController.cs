using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace GestaoDeConcessionaria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CepController(IHttpClientFactory httpClientFactory) : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

        [HttpGet("{cep}")]
        [AllowAnonymous]
        public async Task<IActionResult> BuscarCep(string cep)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    using var document = JsonDocument.Parse(json);
                    if (document.RootElement.TryGetProperty("erro", out var erroProp))
                    {
                        bool isErro = false;
                        if (erroProp.ValueKind == JsonValueKind.String)
                        {
                            isErro = erroProp.GetString()?.ToLower() == "true";
                        }
                        else if (erroProp.ValueKind == JsonValueKind.True || erroProp.ValueKind == JsonValueKind.False)
                        {
                            isErro = erroProp.GetBoolean();
                        }
                        if (isErro)
                        {
                            return BadRequest(new { Message = "CEP não encontrado." });
                        }
                    }
                    return Ok(JsonDocument.Parse(json).RootElement);
                }
                return BadRequest(new { Message = "CEP inválido ou API de CEP indisponível." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Erro ao buscar CEP: " + ex.Message });
            }
        }
    }
}
