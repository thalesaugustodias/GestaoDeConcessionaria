using GestaoDeConcessionaria.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NToastNotify;

namespace GestaoDeConcessionaria.Web.Controllers
{
    public class CepController(IHttpClientFactory httpClientFactory, IToastNotification toastNotification) : Controller
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("ApiClient");
        private readonly IToastNotification _toastNotification = toastNotification;

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Buscar(string cep)
        {
            if (string.IsNullOrWhiteSpace(cep))
            {
                string errorMessage = "CEP não informado.";
                _toastNotification.AddErrorToastMessageCustom(errorMessage);
                return BadRequest(new { Message = errorMessage });
            }

            var response = await _httpClient.GetAsync($"api/cep/{cep}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return Content(json, "application/json");
            }
            else
            {
                var errorJson = await response.Content.ReadAsStringAsync();
                string errorMessage = "Erro ao buscar CEP no serviço externo.";
                try
                {
                    var errorObj = JsonConvert.DeserializeObject<dynamic>(errorJson);
                    errorMessage = errorObj?.Message ?? errorMessage;
                }
                catch { }

                _toastNotification.AddErrorToastMessageCustom(errorMessage);
                return BadRequest(new { Message = errorMessage });
            }
        }
    }
}
