using GestaoDeConcessionaria.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NToastNotify;

namespace GestaoDeConcessionaria.Web.Controllers
{
    [Authorize(Roles = "Gerente")]
    public class VeiculosController(IHttpClientFactory httpClientFactory, IToastNotification toastNotification) : Controller
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("ApiClient");
        private readonly IToastNotification _toastNotification = toastNotification;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/veiculos");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var veiculos = JsonConvert.DeserializeObject<IEnumerable<VeiculoViewModel>>(jsonData);
                return View(veiculos);
            }
            _toastNotification.AddErrorToastMessage("Erro ao carregar veículos.");
            return View(new List<VeiculoViewModel>());
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"api/veiculos/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var veiculo = JsonConvert.DeserializeObject<VeiculoViewModel>(jsonData);
                return View(veiculo);
            }
            _toastNotification.AddErrorToastMessage("Veículo não encontrado.");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new VeiculoViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VeiculoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(model),System.Text.Encoding.UTF8,"application/json");

                var response = await _httpClient.PostAsync("api/veiculos", content);
                if (response.IsSuccessStatusCode)
                {
                    _toastNotification.AddSuccessToastMessage("Veículo criado com sucesso!");
                    return RedirectToAction("Index");
                }
                else
                {
                    var jsonError = await response.Content.ReadAsStringAsync();
                    string errorMessage = "Erro ao criar veículo.";
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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"api/veiculos/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var veiculo = JsonConvert.DeserializeObject<VeiculoViewModel>(jsonData);
                return View(veiculo);
            }
            _toastNotification.AddErrorToastMessage("Veículo não encontrado para edição.");
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VeiculoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(model),System.Text.Encoding.UTF8,"application/json");

                var response = await _httpClient.PutAsync($"api/veiculos/{id}", content);
                if (response.IsSuccessStatusCode)
                {
                    _toastNotification.AddSuccessToastMessage("Veículo atualizado com sucesso!");
                    return RedirectToAction("Index");
                }
                else
                {
                    var jsonError = await response.Content.ReadAsStringAsync();
                    string errorMessage = "Erro ao atualizar veículo.";
                    try
                    {
                        var errorObj = JsonConvert.DeserializeObject<dynamic>(jsonError);
                        errorMessage = errorObj?.Message ?? errorMessage;
                    }
                    catch { }
                    _toastNotification.AddErrorToastMessage(errorMessage);
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
                var veiculo = JsonConvert.DeserializeObject<VeiculoViewModel>(jsonData);
                return View(veiculo);
            }
            _toastNotification.AddErrorToastMessage("Veículo não encontrado para exclusão.");
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/veiculos/{id}");
            if (response.IsSuccessStatusCode)
            {
                _toastNotification.AddSuccessToastMessage("Veículo removido com sucesso!");
                return RedirectToAction("Index");
            }
            else
            {
                var jsonError = await response.Content.ReadAsStringAsync();
                string errorMessage = "Erro ao remover veículo.";
                try
                {
                    var errorObj = JsonConvert.DeserializeObject<dynamic>(jsonError);
                    errorMessage = errorObj?.Message ?? errorMessage;
                }
                catch { }
                _toastNotification.AddErrorToastMessage(errorMessage);
                return RedirectToAction("Index");
            }
        }
    }
}