using GestaoDeConcessionaria.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GestaoDeConcessionaria.Web.Controllers
{
    public class VendasController : Controller
    {
        private readonly HttpClient _httpClient;

        public VendasController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/vendas");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var vendas = JsonConvert.DeserializeObject<IEnumerable<VendaViewModel>>(jsonData);
                return View(vendas);
            }
            return View(new List<VendaViewModel>());
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"api/vendas/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var venda = JsonConvert.DeserializeObject<VendaViewModel>(jsonData);
                return View(venda);
            }
            return NotFound();
        }

        public IActionResult Create()
        {
            return View(new VendaViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(VendaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(model), System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/vendas", content);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");
                ModelState.AddModelError("", "Erro ao criar venda.");
            }
            return View(model);
        }
    }
}
