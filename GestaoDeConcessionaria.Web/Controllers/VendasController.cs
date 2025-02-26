using GestaoDeConcessionaria.Domain.Entities;
using GestaoDeConcessionaria.Web.Extensions;
using GestaoDeConcessionaria.Web.Models;
using GestaoDeConcessionaria.Web.Models.Vendas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Text.Json;
using static GestaoDeConcessionaria.Web.Factories.VendaFactory;

namespace GestaoDeConcessionaria.Web.Controllers
{
    [Authorize(Roles = "Vendedor")]
    public class VendasController(IHttpClientFactory httpClientFactory, IToastNotification toastNotification) : Controller
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("ApiClient");
        private readonly IToastNotification _toastNotification = toastNotification;
        private readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/vendas");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var vendasDomain = JsonSerializer.Deserialize<IEnumerable<Venda>>(jsonData, _jsonSerializerOptions);
                var vendasViewModel = VendaViewModelFactory.Create(vendasDomain!);
                return View(vendasViewModel);
            }
            _toastNotification.AddErrorToastMessageCustom("Erro ao carregar vendas.");
            return View(new List<VendaViewModel>());
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (!ModelState.IsValid)
            {
                _toastNotification.AddErrorToastMessageCustom("Dados inválidos.");
                return RedirectToAction("Index");
            }

            var response = await _httpClient.GetAsync($"api/vendas/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var venda = JsonSerializer.Deserialize<VendaDetalhesViewModel>(jsonData, _jsonSerializerOptions);
                return View(venda);
            }
            _toastNotification.AddErrorToastMessageCustom("Venda não encontrada.");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var response = await _httpClient.GetAsync("api/vendas/obter-dados-para-criacao");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var dados = JsonSerializer.Deserialize<VendaCreateViewModel>(jsonData, _jsonSerializerOptions);
                ViewBag.Veiculos = dados?.Veiculos ?? [];
                ViewBag.Concessionarias = dados?.Concessionarias ?? [];
                ViewBag.Clientes = dados?.Clientes ?? [];
            }
            else
            {
                ViewBag.Veiculos = new List<VeiculoViewModel>();
                ViewBag.Concessionarias = new List<ConcessionariaViewModel>();
                ViewBag.Clientes = new List<ClienteViewModel>();
            }

            var model = new VendaViewModel { DataVenda = DateTime.Today };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VendaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonSerializer.Serialize(model, _jsonSerializerOptions), System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/vendas", content);
                if (response.IsSuccessStatusCode)
                {
                    _toastNotification.AddSuccessToastMessageCustom("Venda criada com sucesso!");
                    return RedirectToAction("Index");
                }
                else
                {
                    var jsonError = await response.Content.ReadAsStringAsync();
                    string errorMessage = "Erro ao criar venda.";
                    try
                    {
                        var errorObj = JsonSerializer.Deserialize<dynamic>(jsonError, _jsonSerializerOptions);
                        errorMessage = errorObj?.Message ?? errorMessage;
                    }
                    catch (JsonException ex)
                    {
                        Console.WriteLine();
                        _toastNotification.AddErrorToastMessageCustom($"JSON deserialization error: {ex.Message}");
                    }

                    _toastNotification.AddErrorToastMessageCustom(errorMessage);
                    return RedirectToAction("Create");
                }
            }
            return View(model);
        }
    }
}