using GestaoDeConcessionaria.Application.DTOs;
using GestaoDeConcessionaria.Application.Factories;
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
        public async Task<IActionResult> Adicionar([FromBody] ConcessionariaDTO concessionariaDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var client = _clientFactory.CreateClient();
                var response = await client.GetAsync($"https://viacep.com.br/ws/{concessionariaDto.CEP}/json/");
                if (!response.IsSuccessStatusCode)
                    return BadRequest(new { Message = "CEP inválido ou API de CEP indisponível." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Erro ao validar CEP: " + ex.Message });
            }

            try
            {
                var concessionaria = ConcessionariaFactory.Criar(concessionariaDto);
                await _servicoConcessionaria.AdicionarAsync(concessionaria);
                await _cache.RemoveAsync("lista_concessionarias");
                return CreatedAtAction(nameof(ObterPorId), new { id = concessionaria.Id }, concessionaria);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro ao adicionar concessionária: " + ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] ConcessionariaDTO concessionariaDto)
        {
            try
            {
                var concessionariaExistente = await _servicoConcessionaria.ObterPorIdAsync(id);
                if (concessionariaExistente == null)
                    return NotFound();

                ConcessionariaFactory.Atualizar(concessionariaExistente, concessionariaDto);
                await _servicoConcessionaria.AtualizarAsync(concessionariaExistente);
                await _cache.RemoveAsync("lista_concessionarias");
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro ao atualizar concessionária: " + ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            try
            {
                await _servicoConcessionaria.DeletarAsync(id);
                await _cache.RemoveAsync("lista_concessionarias");
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro ao remover concessionária: " + ex.Message });
            }
        }
    }
}