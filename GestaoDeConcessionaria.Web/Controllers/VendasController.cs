using GestaoDeConcessionaria.Domain.Entities;
using GestaoDeConcessionaria.Web.Extensions;
using GestaoDeConcessionaria.Web.Models;
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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/vendas");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var vendasDomain = JsonSerializer.Deserialize<IEnumerable<Venda>>(jsonData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                var vendasViewModel = VendaViewModelFactory.Create(vendasDomain);
                return View(vendasViewModel);
            }
            _toastNotification.AddErrorToastMessageCustom("Erro ao carregar vendas.");
            return View(new List<VendaViewModel>());
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"api/vendas/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var venda = JsonSerializer.Deserialize<VendaViewModel>(jsonData);
                return View(venda);
            }
            _toastNotification.AddErrorToastMessageCustom("Venda não encontrada.");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var veiculosResponse = await _httpClient.GetAsync("api/veiculos");
            var concessionariasResponse = await _httpClient.GetAsync("api/concessionarias");
            var clientesResponse = await _httpClient.GetAsync("api/clientes");

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if (veiculosResponse.IsSuccessStatusCode && concessionariasResponse.IsSuccessStatusCode && clientesResponse.IsSuccessStatusCode)
            {
                var veiculosJson = await veiculosResponse.Content.ReadAsStringAsync();
                var concessionariasJson = await concessionariasResponse.Content.ReadAsStringAsync();
                var clientesJson = await clientesResponse.Content.ReadAsStringAsync();

                var veiculos = JsonSerializer.Deserialize<IEnumerable<VeiculoViewModel>>(veiculosJson, options);
                var concessionarias = JsonSerializer.Deserialize<IEnumerable<ConcessionariaViewModel>>(concessionariasJson, options);
                var clientes = JsonSerializer.Deserialize<IEnumerable<ClienteViewModel>>(clientesJson, options);

                ViewBag.Veiculos = veiculos ?? [];
                ViewBag.Concessionarias = concessionarias ?? [];
                ViewBag.Clientes = clientes ?? [];
            }
            else
            {
                ViewBag.Veiculos = new List<VeiculoViewModel>();
                ViewBag.Concessionarias = new List<ConcessionariaViewModel>();
                ViewBag.Clientes = new List<ClienteViewModel>();
            }

            return View(new VendaViewModel());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VendaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonSerializer.Serialize(model), System.Text.Encoding.UTF8, "application/json");
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
                        var errorObj = JsonSerializer.Deserialize<dynamic>(jsonError);
                        errorMessage = errorObj?.Message ?? errorMessage;
                    }
                    catch { }
                    _toastNotification.AddErrorToastMessageCustom(errorMessage);
                    return RedirectToAction("Create");
                }
            }
            return View(model);
        }
    }
}