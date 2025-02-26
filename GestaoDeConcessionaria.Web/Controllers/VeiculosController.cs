using GestaoDeConcessionaria.Domain.Enums;
using GestaoDeConcessionaria.Web.Extensions;
using GestaoDeConcessionaria.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Text;
using System.Text.Json;

namespace GestaoDeConcessionaria.Web.Controllers
{
    [Authorize(Roles = "Gerente")]
    public class VeiculosController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IToastNotification _toastNotification;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public VeiculosController(IHttpClientFactory httpClientFactory, IToastNotification toastNotification)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _toastNotification = toastNotification;
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/veiculos");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var veiculos = JsonSerializer.Deserialize<IEnumerable<VeiculoViewModel>>(jsonData, _jsonSerializerOptions);
                return View(veiculos);
            }
            _toastNotification.AddErrorToastMessageCustom("Erro ao carregar veículos.");
            return View(new List<VeiculoViewModel>());
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetPreco(int veiculoId)
        {
            var response = await _httpClient.GetAsync($"api/veiculos/{veiculoId}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var veiculo = JsonSerializer.Deserialize<VeiculoViewModel>(jsonData, _jsonSerializerOptions);
                if (veiculo != null)
                {
                    return Json(new { preco = veiculo.Preco });
                }
            }
            return Json(new { preco = 0 });
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"api/veiculos/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var veiculo = JsonSerializer.Deserialize<VeiculoViewModel>(jsonData, _jsonSerializerOptions);
                if (veiculo != null)
                {
                    if (veiculo.FabricanteId > 0)
                    {
                        var fabResponse = await _httpClient.GetAsync($"api/fabricantes/{veiculo.FabricanteId}");
                        if (fabResponse.IsSuccessStatusCode)
                        {
                            var fabJson = await fabResponse.Content.ReadAsStringAsync();
                            var fabricante = JsonSerializer.Deserialize<FabricanteViewModel>(fabJson, _jsonSerializerOptions);
                            ViewBag.NomeFabricante = fabricante?.Nome;
                        }
                        else
                        {
                            ViewBag.NomeFabricante = "Não especificado";
                        }
                    }
                    else
                    {
                        ViewBag.NomeFabricante = "Não especificado";
                    }
                    return View(veiculo);
                }
            }
            _toastNotification.AddErrorToastMessageCustom("Veículo não encontrado.");
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.TiposVeiculos = Enum.GetValues(typeof(TipoVeiculo))
                .Cast<TipoVeiculo>()
                .Select(t => new { Value = t, Text = t.ToString() })
                .ToList();

            var fabricantesResponse = await _httpClient.GetAsync("api/fabricantes");
            IEnumerable<FabricanteViewModel> fabricantes;
            if (fabricantesResponse.IsSuccessStatusCode)
            {
                var json = await fabricantesResponse.Content.ReadAsStringAsync();
                fabricantes = JsonSerializer.Deserialize<IEnumerable<FabricanteViewModel>>(json, _jsonSerializerOptions);
            }
            else
            {
                fabricantes = [];
            }
            ViewBag.Fabricantes = fabricantes ?? new List<FabricanteViewModel>();

            return View(new VeiculoViewModel());
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VeiculoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonSerializer.Serialize(model, _jsonSerializerOptions), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/veiculos", content);
                if (response.IsSuccessStatusCode)
                {
                    _toastNotification.AddSuccessToastMessageCustom("Veículo criado com sucesso!");
                    return RedirectToAction("Index");
                }
                else
                {
                    var jsonError = await response.Content.ReadAsStringAsync();
                    string errorMessage = "Erro ao criar veículo.";
                    try
                    {
                        using var doc = JsonDocument.Parse(jsonError);
                        if (doc.RootElement.TryGetProperty("Message", out JsonElement element))
                        {
                            errorMessage = element.GetString() ?? errorMessage;
                        }
                    }
                    catch { }
                    _toastNotification.AddErrorToastMessageCustom(errorMessage);
                    return RedirectToAction("Create");
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"api/veiculos/{id}");
            if (!response.IsSuccessStatusCode)
            {
                _toastNotification.AddErrorToastMessageCustom("Veículo não encontrado para edição.");
                return RedirectToAction("Index");
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            var veiculo = JsonSerializer.Deserialize<VeiculoViewModel>(jsonData, _jsonSerializerOptions);
            var fabricantesResponse = await _httpClient.GetAsync("api/fabricantes");
            IEnumerable<FabricanteViewModel> fabricantes;
            if (fabricantesResponse.IsSuccessStatusCode)
            {
                var fabricantesJson = await fabricantesResponse.Content.ReadAsStringAsync();
                fabricantes = JsonSerializer.Deserialize<IEnumerable<FabricanteViewModel>>(fabricantesJson, _jsonSerializerOptions)
                              ?? [];
            }
            else
            {
                fabricantes = [];
            }
            ViewBag.Fabricantes = fabricantes;

            ViewBag.TiposVeiculos = Enum.GetValues(typeof(TipoVeiculo))
                .Cast<TipoVeiculo>()
                .Select(t => new { Value = t, Text = t.ToString() })
                .ToList();

            return View(veiculo);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VeiculoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonSerializer.Serialize(model, _jsonSerializerOptions), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/veiculos/{id}", content);
                if (response.IsSuccessStatusCode)
                {
                    _toastNotification.AddSuccessToastMessageCustom("Veículo atualizado com sucesso!");
                    return RedirectToAction("Index");
                }
                else
                {
                    var jsonError = await response.Content.ReadAsStringAsync();
                    string errorMessage = "Erro ao atualizar veículo.";
                    try
                    {
                        using var doc = JsonDocument.Parse(jsonError);
                        if (doc.RootElement.TryGetProperty("Message", out JsonElement element))
                        {
                            errorMessage = element.GetString() ?? errorMessage;
                        }
                    }
                    catch { }
                    _toastNotification.AddErrorToastMessageCustom(errorMessage);
                    return RedirectToAction("Edit", new { id });
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"api/veiculos/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var veiculo = JsonSerializer.Deserialize<VeiculoViewModel>(jsonData, _jsonSerializerOptions);
                return View(veiculo);
            }
            _toastNotification.AddErrorToastMessageCustom("Veículo não encontrado para exclusão.");
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/veiculos/{id}");
            if (response.IsSuccessStatusCode)
            {
                _toastNotification.AddSuccessToastMessageCustom("Veículo removido com sucesso!");
                return RedirectToAction("Index");
            }
            else
            {
                var jsonError = await response.Content.ReadAsStringAsync();
                string errorMessage = "Erro ao remover veículo.";
                try
                {
                    using var doc = JsonDocument.Parse(jsonError);
                    if (doc.RootElement.TryGetProperty("Message", out JsonElement element))
                    {
                        errorMessage = element.GetString() ?? errorMessage;
                    }
                }
                catch { }
                _toastNotification.AddErrorToastMessageCustom(errorMessage);
                return RedirectToAction("Index");
            }
        }
    }
}
