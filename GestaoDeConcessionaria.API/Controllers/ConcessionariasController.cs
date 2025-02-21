﻿using GestaoDeConcessionaria.Application.Interfaces;
using GestaoDeConcessionaria.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace GestaoDeConcessionaria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrador")]
    public class ConcessionariasController(IConcessionariaService servicoConcessionaria, IDistributedCache cache, IHttpClientFactory clientFactory) : ControllerBase
    {
        private readonly IConcessionariaService _servicoConcessionaria = servicoConcessionaria;
        private readonly IDistributedCache _cache = cache;
        private readonly IHttpClientFactory _clientFactory = clientFactory;

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ObterTodos()
        {
            var cacheKey = "lista_concessionarias";
            string jsonLista = await _cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(jsonLista))
            {
                var concessionariasCache = JsonSerializer.Deserialize<IEnumerable<Concessionaria>>(jsonLista);
                return Ok(concessionariasCache);
            }

            var concessionarias = await _servicoConcessionaria.ObterTodosAsync();
            jsonLista = JsonSerializer.Serialize(concessionarias);
            var options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5));
            await _cache.SetStringAsync(cacheKey, jsonLista, options);
            return Ok(concessionarias);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var concessionaria = await _servicoConcessionaria.ObterPorIdAsync(id);
            if (concessionaria == null)
                return NotFound();
            return Ok(concessionaria);
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] Concessionaria concessionaria)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Exemplo de integração com API externa (ViaCEP) para validar o CEP.
            try
            {
                var client = _clientFactory.CreateClient();
                var response = await client.GetAsync($"https://viacep.com.br/ws/{concessionaria.CEP}/json/");
                if (!response.IsSuccessStatusCode)
                {
                    return BadRequest("CEP inválido ou API de CEP indisponível.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao validar CEP: " + ex.Message);
            }

            await _servicoConcessionaria.AdicionarAsync(concessionaria);
            await _cache.RemoveAsync("lista_concessionarias");
            return CreatedAtAction(nameof(ObterPorId), new { id = concessionaria.Id }, concessionaria);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] Concessionaria concessionaria)
        {
            if (id != concessionaria.Id)
                return BadRequest("Id divergente.");
            await _servicoConcessionaria.AtualizarAsync(concessionaria);
            await _cache.RemoveAsync("lista_concessionarias");
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            await _servicoConcessionaria.DeletarAsync(id);
            await _cache.RemoveAsync("lista_concessionarias");
            return NoContent();
        }
    }
}
