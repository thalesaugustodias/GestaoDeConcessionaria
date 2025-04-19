using GestaoDeConcessionaria.Application.DTOs;
using GestaoDeConcessionaria.Application.Factories;
using GestaoDeConcessionaria.Application.Interfaces;
using GestaoDeConcessionaria.Application.Services;
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
    public class VeiculosController(IVeiculoService servicoVeiculo, IFabricanteService servicoFabricante, IDistributedCache cache) : ControllerBase
    {
        private readonly IVeiculoService _servicoVeiculo = servicoVeiculo;
        private readonly IFabricanteService _servicoFabricante = servicoFabricante;
        private readonly IDistributedCache _cache = cache;

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<VeiculoDto>>> ObterTodos()
        {
            const string cacheKey = "lista_veiculos";
            string? json = await _cache.GetStringAsync(cacheKey);
            IEnumerable<Veiculo> raw;

            if (!string.IsNullOrEmpty(json))
            {
                raw = JsonSerializer.Deserialize<IEnumerable<Veiculo>>(json)!;
            }
            else
            {
                raw = await _servicoVeiculo.ObterTodosAsync();
                json = JsonSerializer.Serialize(raw);
                var opts = new DistributedCacheEntryOptions()
                               .SetSlidingExpiration(TimeSpan.FromMinutes(5));
                await _cache.SetStringAsync(cacheKey, json, opts);
            }

            var dtos = VeiculoFactory.CreateList(raw);
            return Ok(dtos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<VeiculoDto>> ObterPorId(int id)
        {
            var v = await _servicoVeiculo.ObterPorIdAsync(id);
            if (v is null) return NotFound();
            return Ok(VeiculoFactory.Create(v));
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] VeiculoDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var fabricante = await _servicoFabricante.ObterPorIdAsync(dto.FabricanteId);
            if (fabricante == null)
                return BadRequest(new { Message = "Fabricante não encontrado." });

            try
            {
                var veiculo = VeiculoFactory.CriarVeiculo(dto, fabricante);
                await _servicoVeiculo.AdicionarAsync(veiculo);
                await _cache.RemoveAsync("lista_veiculos");
                return CreatedAtAction(nameof(ObterPorId), new { id = veiculo.Id }, veiculo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro ao adicionar veículo: " + ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] VeiculoDto dto)
        {
            try
            {
                var veiculoExistente = await _servicoVeiculo.ObterPorIdAsync(id);
                if (veiculoExistente == null)
                    return NotFound();

                var fabricante = await _servicoFabricante.ObterPorIdAsync(dto.FabricanteId);
                if (fabricante == null)
                    return BadRequest(new { Message = "Fabricante não encontrado." });

                VeiculoFactory.Atualizar(veiculoExistente, dto, fabricante);
                await _servicoVeiculo.AtualizarAsync(veiculoExistente);
                await _cache.RemoveAsync("lista_veiculos");
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro ao atualizar veículo: " + ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            try
            {
                await _servicoVeiculo.DeletarAsync(id);
                await _cache.RemoveAsync("lista_veiculos");
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro ao remover veículo: " + ex.Message });
            }
        }
    }
}