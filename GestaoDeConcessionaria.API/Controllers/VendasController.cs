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
    [Authorize(Roles = "Vendedor")]
    public class VendasController(IVendaService servicoVenda, IDistributedCache cache) : ControllerBase
    {
        private readonly IVendaService _servicoVenda = servicoVenda;
        private readonly IDistributedCache _cache = cache;

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ObterTodos()
        {
            var cacheKey = "lista_vendas";
            string jsonLista = await _cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(jsonLista))
            {
                var vendasCache = JsonSerializer.Deserialize<IEnumerable<Venda>>(jsonLista);
                return Ok(vendasCache);
            }

            var vendas = await _servicoVenda.ObterTodosAsync();
            jsonLista = JsonSerializer.Serialize(vendas);
            var options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5));
            await _cache.SetStringAsync(cacheKey, jsonLista, options);
            return Ok(vendas);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var venda = await _servicoVenda.ObterPorIdAsync(id);
            if (venda == null)
                return NotFound();
            return Ok(venda);
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] Venda venda)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _servicoVenda.AdicionarAsync(venda);
            await _cache.RemoveAsync("lista_vendas");
            return CreatedAtAction(nameof(ObterPorId), new { id = venda.Id }, venda);
        }
    }
}
