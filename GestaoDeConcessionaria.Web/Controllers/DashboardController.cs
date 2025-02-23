using GestaoDeConcessionaria.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GestaoDeConcessionaria.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly HttpClient _httpClient;

        public DashboardController(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            var token = httpContextAccessor?.HttpContext?.Session.GetString("JWToken");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }

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
                model = new DashboardViewModel
                {
                    TotalVendas = 0,
                    Faturamento = 0,
                    TotalVeiculos = 0,
                    TotalClientes = 0,
                    VendasMensais = new List<ChartData>(),
                    VendasPorFabricante = new List<ChartData>()
                };
            }
            return View(model);
        }
    }
}
