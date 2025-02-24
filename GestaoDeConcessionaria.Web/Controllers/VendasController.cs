using GestaoDeConcessionaria.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NToastNotify;

namespace GestaoDeConcessionaria.Web.Controllers
{
    [Authorize(Roles = "Vendedor")]
    public class VendasController(IHttpClientFactory httpClientFactory, IToastNotification toastNotification) : Controller
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("ApiClient");
        private readonly IToastNotification _toastNotification = toastNotification;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/vendas");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var vendas = JsonConvert.DeserializeObject<IEnumerable<VendaViewModel>>(jsonData);
                return View(vendas);
            }
            _toastNotification.AddErrorToastMessage("Erro ao carregar vendas.");
            return View(new List<VendaViewModel>());
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"api/vendas/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var venda = JsonConvert.DeserializeObject<VendaViewModel>(jsonData);
                return View(venda);
            }
            _toastNotification.AddErrorToastMessage("Venda não encontrada.");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new VendaViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VendaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(model),System.Text.Encoding.UTF8,"application/json");
                var response = await _httpClient.PostAsync("api/vendas", content);
                if (response.IsSuccessStatusCode)
                {
                    _toastNotification.AddSuccessToastMessage("Venda criada com sucesso!");
                    return RedirectToAction("Index");
                }
                else
                {
                    var jsonError = await response.Content.ReadAsStringAsync();
                    string errorMessage = "Erro ao criar venda.";
                    try
                    {
                        var errorObj = JsonConvert.DeserializeObject<dynamic>(jsonError);
                        errorMessage = errorObj?.Message ?? errorMessage;
                    }
                    catch { }
                    _toastNotification.AddErrorToastMessage(errorMessage);
                    return RedirectToAction("Create");
                }
            }
            return View(model);
        }
    }
}