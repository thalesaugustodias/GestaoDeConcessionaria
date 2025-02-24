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
    public class FabricantesController(IFabricanteService servicoFabricante, IDistributedCache cache) : ControllerBase
    {
        private readonly IFabricanteService _servicoFabricante = servicoFabricante;
        private readonly IDistributedCache _cache = cache;

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ObterTodos()
        {
            var cacheKey = "lista_fabricantes";
            string jsonLista = await _cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(jsonLista))
            {
                var fabricantesCache = JsonSerializer.Deserialize<IEnumerable<Fabricante>>(jsonLista);
                return Ok(fabricantesCache);
            }

            var fabricantes = await _servicoFabricante.ObterTodosAsync();
            jsonLista = JsonSerializer.Serialize(fabricantes);
            var options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5));
            await _cache.SetStringAsync(cacheKey, jsonLista, options);
            return Ok(fabricantes);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var fabricante = await _servicoFabricante.ObterPorIdAsync(id);
            if (fabricante == null)
                return NotFound();
            return Ok(fabricante);
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] FabricanteDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var fabricante = FabricanteFactory.Criar(dto);
                await _servicoFabricante.AdicionarAsync(fabricante);
                await _cache.RemoveAsync("lista_fabricantes");
                return CreatedAtAction(nameof(ObterPorId), new { id = fabricante.Id }, fabricante);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Ocorreu um erro interno. Por favor, tente novamente mais tarde." });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] FabricanteDTO dto)
        {
            try
            {
                var fabricanteExistente = await _servicoFabricante.ObterPorIdAsync(id);
                if (fabricanteExistente == null)
                    return NotFound();

                FabricanteFactory.Atualizar(fabricanteExistente, dto);
                await _servicoFabricante.AtualizarAsync(fabricanteExistente);
                await _cache.RemoveAsync("lista_fabricantes");
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Ocorreu um erro interno. Por favor, tente novamente mais tarde." });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            try
            {
                await _servicoFabricante.DeletarAsync(id);
                await _cache.RemoveAsync("lista_fabricantes");
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Ocorreu um erro interno. Por favor, tente novamente mais tarde." });
            }
        }
    }
}