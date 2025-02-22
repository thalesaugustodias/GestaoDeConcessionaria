using GestaoDeConcessionaria.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GestaoDeConcessionaria.Web.Controllers
{
    public class FabricantesController(IHttpClientFactory httpClientFactory) : Controller
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("ApiClient");

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/fabricantes");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var fabricantes = JsonConvert.DeserializeObject<IEnumerable<FabricanteViewModel>>(jsonData);
                return View(fabricantes);
            }
            return View(new List<FabricanteViewModel>());
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"api/fabricantes/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var fabricante = JsonConvert.DeserializeObject<FabricanteViewModel>(jsonData);
                return View(fabricante);
            }
            return NotFound();
        }

        public IActionResult Create()
        {
            return View(new FabricanteViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(FabricanteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(model), System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/fabricantes", content);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");
                ModelState.AddModelError("", "Erro ao criar fabricante.");
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"api/fabricantes/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var fabricante = JsonConvert.DeserializeObject<FabricanteViewModel>(jsonData);
                return View(fabricante);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, FabricanteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(model), System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/fabricantes/{id}", content);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");
                ModelState.AddModelError("", "Erro ao atualizar fabricante.");
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"api/fabricantes/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var fabricante = JsonConvert.DeserializeObject<FabricanteViewModel>(jsonData);
                return View(fabricante);
            }
            return NotFound();
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/fabricantes/{id}");
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");
            return BadRequest();
        }
    }
}
