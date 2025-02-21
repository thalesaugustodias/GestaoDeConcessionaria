using GestaoDeConcessionaria.Application.Interfaces;
using GestaoDeConcessionaria.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace GestaoDeConcessionaria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Gerente")]
    public class VeiculosController(IVeiculoService servicoVeiculo, IDistributedCache cache) : ControllerBase
    {
        private readonly IVeiculoService _servicoVeiculo = servicoVeiculo;
        private readonly IDistributedCache _cache = cache;

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ObterTodos()
        {
            var cacheKey = "lista_veiculos";
            string jsonLista = await _cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(jsonLista))
            {
                var veiculosCache = JsonSerializer.Deserialize<IEnumerable<Veiculo>>(jsonLista);
                return Ok(veiculosCache);
            }

            var veiculos = await _servicoVeiculo.ObterTodosAsync();
            jsonLista = JsonSerializer.Serialize(veiculos);
            var options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5));
            await _cache.SetStringAsync(cacheKey, jsonLista, options);
            return Ok(veiculos);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var veiculo = await _servicoVeiculo.ObterPorIdAsync(id);
            if (veiculo == null)
                return NotFound();
            return Ok(veiculo);
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] Veiculo veiculo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _servicoVeiculo.AdicionarAsync(veiculo);
            await _cache.RemoveAsync("lista_veiculos");
            return CreatedAtAction(nameof(ObterPorId), new { id = veiculo.Id }, veiculo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] Veiculo veiculo)
        {
            if (id != veiculo.Id)
                return BadRequest("Id divergente.");
            await _servicoVeiculo.AtualizarAsync(veiculo);
            await _cache.RemoveAsync("lista_veiculos");
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            await _servicoVeiculo.DeletarAsync(id);
            await _cache.RemoveAsync("lista_veiculos");
            return NoContent();
        }
    }
}
