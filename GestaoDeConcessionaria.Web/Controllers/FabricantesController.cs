using GestaoDeConcessionaria.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NToastNotify;

namespace GestaoDeConcessionaria.Web.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class FabricantesController(IHttpClientFactory httpClientFactory, IToastNotification toastNotification) : Controller
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("ApiClient");
        private readonly IToastNotification _toastNotification = toastNotification;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/fabricantes");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var fabricantes = JsonConvert.DeserializeObject<IEnumerable<FabricanteViewModel>>(jsonData);
                return View(fabricantes);
            }
            _toastNotification.AddErrorToastMessage("Erro ao carregar fabricantes.");
            return View(new List<FabricanteViewModel>());
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"api/fabricantes/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var fabricante = JsonConvert.DeserializeObject<FabricanteViewModel>(jsonData);
                return View(fabricante);
            }
            _toastNotification.AddErrorToastMessage("Fabricante não encontrado.");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new FabricanteViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FabricanteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(
                    JsonConvert.SerializeObject(model),
                    System.Text.Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PostAsync("api/fabricantes", content);
                if (response.IsSuccessStatusCode)
                {
                    _toastNotification.AddSuccessToastMessage("Fabricante criado com sucesso!");
                    return RedirectToAction("Index");
                }
                else
                {
                    var jsonError = await response.Content.ReadAsStringAsync();
                    string errorMessage = "Erro ao criar fabricante.";
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
            var response = await _httpClient.GetAsync($"api/fabricantes/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var fabricante = JsonConvert.DeserializeObject<FabricanteViewModel>(jsonData);
                return View(fabricante);
            }
            _toastNotification.AddErrorToastMessage("Fabricante não encontrado para edição.");
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FabricanteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(
                    JsonConvert.SerializeObject(model),
                    System.Text.Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PutAsync($"api/fabricantes/{id}", content);
                if (response.IsSuccessStatusCode)
                {
                    _toastNotification.AddSuccessToastMessage("Fabricante atualizado com sucesso!");
                    return RedirectToAction("Index");
                }
                else
                {
                    var jsonError = await response.Content.ReadAsStringAsync();
                    string errorMessage = "Erro ao atualizar fabricante.";
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/fabricantes/{id}");
            if (response.IsSuccessStatusCode)
            {
                _toastNotification.AddSuccessToastMessage("Fabricante removido com sucesso!");
                return RedirectToAction("Index");
            }
            else
            {
                var jsonError = await response.Content.ReadAsStringAsync();
                string errorMessage = "Erro ao remover fabricante.";
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