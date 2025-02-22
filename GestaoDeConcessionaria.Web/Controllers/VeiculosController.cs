using GestaoDeConcessionaria.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GestaoDeConcessionaria.Web.Controllers
{
    public class VeiculosController(IHttpClientFactory httpClientFactory) : Controller
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("ApiClient");

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/veiculos");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var veiculos = JsonConvert.DeserializeObject<IEnumerable<VeiculoViewModel>>(jsonData);
                return View(veiculos);
            }
            return View(new List<VeiculoViewModel>());
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"api/veiculos/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var veiculo = JsonConvert.DeserializeObject<VeiculoViewModel>(jsonData);
                return View(veiculo);
            }
            return NotFound();
        }

        public IActionResult Create()
        {
            return View(new VeiculoViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(VeiculoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(model), System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/veiculos", content);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");
                ModelState.AddModelError("", "Erro ao criar veículo.");
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"api/veiculos/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var veiculo = JsonConvert.DeserializeObject<VeiculoViewModel>(jsonData);
                return View(veiculo);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, VeiculoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(model), System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/veiculos/{id}", content);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");
                ModelState.AddModelError("", "Erro ao atualizar veículo.");
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"api/veiculos/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var veiculo = JsonConvert.DeserializeObject<VeiculoViewModel>(jsonData);
                return View(veiculo);
            }
            return NotFound();
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/veiculos/{id}");
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");
            return BadRequest();
        }
    }
}
