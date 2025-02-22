using GestaoDeConcessionaria.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GestaoDeConcessionaria.Web.Controllers
{
    public class ClientesController(IHttpClientFactory httpClientFactory) : Controller
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("ApiClient");

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/clientes");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var clientes = JsonConvert.DeserializeObject<IEnumerable<ClienteViewModel>>(jsonData);
                return View(clientes);
            }
            return View(new List<ClienteViewModel>());
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"api/clientes/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var cliente = JsonConvert.DeserializeObject<ClienteViewModel>(jsonData);
                return View(cliente);
            }
            return NotFound();
        }

        public IActionResult Create()
        {
            return View(new ClienteViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClienteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(model), System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/clientes", content);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");
                ModelState.AddModelError("", "Erro ao criar cliente.");
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"api/clientes/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var cliente = JsonConvert.DeserializeObject<ClienteViewModel>(jsonData);
                return View(cliente);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ClienteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(model), System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/clientes/{id}", content);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");
                ModelState.AddModelError("", "Erro ao atualizar cliente.");
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"api/clientes/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var cliente = JsonConvert.DeserializeObject<ClienteViewModel>(jsonData);
                return View(cliente);
            }
            return NotFound();
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/clientes/{id}");
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");
            return BadRequest();
        }
    }
}
