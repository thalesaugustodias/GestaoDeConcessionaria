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
    [Authorize(Roles = "Gerente")]
    public class VeiculosController(IVeiculoService servicoVeiculo, IFabricanteService servicoFabricante, IDistributedCache cache) : ControllerBase
    {
        private readonly IVeiculoService _servicoVeiculo = servicoVeiculo;
        private readonly IFabricanteService _servicoFabricante = servicoFabricante;
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
        public async Task<IActionResult> Adicionar([FromBody] VeiculoDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var fabricante = await _servicoFabricante.ObterPorIdAsync(dto.FabricanteId);
            if (fabricante == null)
                return BadRequest(new { Message = "Fabricante não encontrado." });

            try
            {
                var veiculo = VeiculoFactory.Criar(dto, fabricante);
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
        public async Task<IActionResult> Atualizar(int id, [FromBody] VeiculoDTO dto)
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