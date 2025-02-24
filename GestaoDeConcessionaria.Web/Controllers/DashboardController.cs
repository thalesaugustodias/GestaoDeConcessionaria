using GestaoDeConcessionaria.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NToastNotify;

namespace GestaoDeConcessionaria.Web.Controllers
{
    public class DashboardController(IHttpClientFactory httpClientFactory, IToastNotification toastNotification) : Controller
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("ApiClient");
        private readonly IToastNotification _toastNotification = toastNotification;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var anoAtual = DateTime.Now.Year;
            var response = await _httpClient.GetAsync($"api/relatorios/mensal?mes=8&ano={anoAtual}");
            DashboardViewModel model;
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<DashboardViewModel>(jsonData);
            }
            else
            {
                _toastNotification.AddErrorToastMessage("Erro ao carregar os dados do relatório.");
                model = new DashboardViewModel
                {
                    TotalVendas = 0,
                    Faturamento = 0,
                    TotalVeiculos = 0,
                    TotalClientes = 0,
                    VendasMensais = [],
                    VendasPorFabricante = []
                };
            }
            return View(model);
        }
    }
}