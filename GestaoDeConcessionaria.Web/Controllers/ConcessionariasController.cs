using GestaoDeConcessionaria.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GestaoDeConcessionaria.Web.Controllers
{
    public class ConcessionariasController(IHttpClientFactory httpClientFactory) : Controller
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("ApiClient");

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/concessionarias");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var concessionarias = JsonConvert.DeserializeObject<IEnumerable<ConcessionariaViewModel>>(jsonData);
                return View(concessionarias);
            }
            return View(new List<ConcessionariaViewModel>());
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"api/concessionarias/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var concessionaria = JsonConvert.DeserializeObject<ConcessionariaViewModel>(jsonData);
                return View(concessionaria);
            }
            return NotFound();
        }

        public IActionResult Create()
        {
            return View(new ConcessionariaViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(ConcessionariaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(model), System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/concessionarias", content);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");
                ModelState.AddModelError("", "Erro ao criar concessionária.");
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"api/concessionarias/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var concessionaria = JsonConvert.DeserializeObject<ConcessionariaViewModel>(jsonData);
                return View(concessionaria);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ConcessionariaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(model), System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/concessionarias/{id}", content);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");
                ModelState.AddModelError("", "Erro ao atualizar concessionária.");
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"api/concessionarias/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var concessionaria = JsonConvert.DeserializeObject<ConcessionariaViewModel>(jsonData);
                return View(concessionaria);
            }
            return NotFound();
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/concessionarias/{id}");
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");
            return BadRequest();
        }
    }
}
