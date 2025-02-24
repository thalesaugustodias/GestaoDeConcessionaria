using GestaoDeConcessionaria.Web.Extensions;
using GestaoDeConcessionaria.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NToastNotify;

namespace GestaoDeConcessionaria.Web.Controllers
{
    [Authorize(Roles = "Administrador,Vendedor,Gerente")]
    public class ClientesController(IHttpClientFactory httpClientFactory, IToastNotification toastNotification) : Controller
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("ApiClient");
        private readonly IToastNotification _toastNotification = toastNotification;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/clientes");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var clientes = JsonConvert.DeserializeObject<IEnumerable<ClienteViewModel>>(jsonData);
                return View(clientes);
            }
            _toastNotification.AddErrorToastMessageCustom("Erro ao carregar clientes.");
            return View(new List<ClienteViewModel>());
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"api/clientes/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var cliente = JsonConvert.DeserializeObject<ClienteViewModel>(jsonData);
                return View(cliente);
            }
            _toastNotification.AddErrorToastMessageCustom("Cliente não encontrado.");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new ClienteViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClienteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(model),System.Text.Encoding.UTF8,"application/json");

                var response = await _httpClient.PostAsync("api/clientes", content);
                if (response.IsSuccessStatusCode)
                {
                    _toastNotification.AddSuccessToastMessageCustom("Cliente criado com sucesso!");
                    return RedirectToAction("Index");
                }
                else
                {
                    var jsonError = await response.Content.ReadAsStringAsync();
                    string errorMessage = "Erro ao criar cliente.";
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
            var response = await _httpClient.GetAsync($"api/clientes/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var cliente = JsonConvert.DeserializeObject<ClienteViewModel>(jsonData);
                return View(cliente);
            }
            _toastNotification.AddErrorToastMessageCustom("Cliente não encontrado para edição.");
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClienteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(model),System.Text.Encoding.UTF8,"application/json");

                var response = await _httpClient.PutAsync($"api/clientes/{id}", content);
                if (response.IsSuccessStatusCode)
                {
                    _toastNotification.AddSuccessToastMessageCustom("Cliente atualizado com sucesso!");
                    return RedirectToAction("Index");
                }
                else
                {
                    var jsonError = await response.Content.ReadAsStringAsync();
                    string errorMessage = "Erro ao atualizar cliente.";
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
            var response = await _httpClient.GetAsync($"api/clientes/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var cliente = JsonConvert.DeserializeObject<ClienteViewModel>(jsonData);
                return View(cliente);
            }
            _toastNotification.AddErrorToastMessageCustom("Cliente não encontrado para exclusão.");
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/clientes/{id}");
            if (response.IsSuccessStatusCode)
            {
                _toastNotification.AddSuccessToastMessageCustom("Cliente removido com sucesso!");
                return RedirectToAction("Index");
            }
            else
            {
                var jsonError = await response.Content.ReadAsStringAsync();
                string errorMessage = "Erro ao remover cliente.";
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