using GestaoDeConcessionaria.Web.Extensions;
using GestaoDeConcessionaria.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NToastNotify;

namespace GestaoDeConcessionaria.Web.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class ConcessionariasController(IHttpClientFactory httpClientFactory, IToastNotification toastNotification) : Controller
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("ApiClient");
        private readonly IToastNotification _toastNotification = toastNotification;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/concessionarias");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var concessionarias = JsonConvert.DeserializeObject<IEnumerable<ConcessionariaViewModel>>(jsonData);
                return View(concessionarias);
            }
            _toastNotification.AddErrorToastMessageCustom("Erro ao carregar concessionárias.");
            return View(new List<ConcessionariaViewModel>());
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"api/concessionarias/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var concessionaria = JsonConvert.DeserializeObject<ConcessionariaViewModel>(jsonData);
                return View(concessionaria);
            }
            _toastNotification.AddErrorToastMessageCustom("Concessionária não encontrada.");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new ConcessionariaViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ConcessionariaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(model),System.Text.Encoding.UTF8,"application/json");
                var response = await _httpClient.PostAsync("api/concessionarias", content);
                if (response.IsSuccessStatusCode)
                {
                    _toastNotification.AddSuccessToastMessageCustom("Concessionária criada com sucesso!");
                    return RedirectToAction("Index");
                }
                else
                {
                    var jsonError = await response.Content.ReadAsStringAsync();
                    string errorMessage = "Erro ao criar concessionária.";
                    try
                    {
                        var errorObj = JsonConvert.DeserializeObject<dynamic>(jsonError);
                        errorMessage = errorObj?.Message ?? errorMessage;
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
            var response = await _httpClient.GetAsync($"api/concessionarias/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var concessionaria = JsonConvert.DeserializeObject<ConcessionariaViewModel>(jsonData);
                return View(concessionaria);
            }
            _toastNotification.AddErrorToastMessageCustom("Concessionária não encontrada para edição.");
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ConcessionariaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(model),System.Text.Encoding.UTF8,"application/json");
                var response = await _httpClient.PutAsync($"api/concessionarias/{id}", content);
                if (response.IsSuccessStatusCode)
                {
                    _toastNotification.AddSuccessToastMessageCustom("Concessionária atualizada com sucesso!");
                    return RedirectToAction("Index");
                }
                else
                {
                    var jsonError = await response.Content.ReadAsStringAsync();
                    string errorMessage = "Erro ao atualizar concessionária.";
                    try
                    {
                        var errorObj = JsonConvert.DeserializeObject<dynamic>(jsonError);
                        errorMessage = errorObj?.Message ?? errorMessage;
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
            var response = await _httpClient.GetAsync($"api/concessionarias/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var concessionaria = JsonConvert.DeserializeObject<ConcessionariaViewModel>(jsonData);
                return View(concessionaria);
            }
            _toastNotification.AddErrorToastMessageCustom("Concessionária não encontrada para exclusão.");
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/concessionarias/{id}");
            if (response.IsSuccessStatusCode)
            {
                _toastNotification.AddSuccessToastMessageCustom("Concessionária removida com sucesso!");
                return RedirectToAction("Index");
            }
            else
            {
                var jsonError = await response.Content.ReadAsStringAsync();
                string errorMessage = "Erro ao remover concessionária.";
                try
                {
                    var errorObj = JsonConvert.DeserializeObject<dynamic>(jsonError);
                    errorMessage = errorObj?.Message ?? errorMessage;
                }
                catch { }
                _toastNotification.AddErrorToastMessageCustom(errorMessage);
                return RedirectToAction("Index");
            }
        }
    }
}