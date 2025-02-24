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
    [Authorize(Roles = "Administrador,Vendedor,Gerente")]
    public class ClientesController(IClienteService servicoCliente, IDistributedCache cache) : ControllerBase
    {
        private readonly IClienteService _servicoCliente = servicoCliente;
        private readonly IDistributedCache _cache = cache;

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ObterTodos()
        {
            var cacheKey = "lista_clientes";
            string jsonLista = await _cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(jsonLista))
            {
                var clientesCache = JsonSerializer.Deserialize<IEnumerable<Cliente>>(jsonLista);
                return Ok(clientesCache);
            }

            var clientes = await _servicoCliente.ObterTodosAsync();
            jsonLista = JsonSerializer.Serialize(clientes);
            var options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5));
            await _cache.SetStringAsync(cacheKey, jsonLista, options);
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var cliente = await _servicoCliente.ObterPorIdAsync(id);
            if (cliente == null)
                return NotFound();
            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] ClienteDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var cliente = ClienteFactory.Criar(dto);
                await _servicoCliente.AdicionarAsync(cliente);
                await _cache.RemoveAsync("lista_clientes");
                return CreatedAtAction(nameof(ObterPorId), new { id = cliente.Id }, cliente);
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
        public async Task<IActionResult> Atualizar(int id, [FromBody] ClienteDTO dto)
        {
            try
            {
                var clienteExistente = await _servicoCliente.ObterPorIdAsync(id);
                if (clienteExistente == null)
                    return NotFound();

                ClienteFactory.Atualizar(clienteExistente, dto);
                await _servicoCliente.AtualizarAsync(clienteExistente);
                await _cache.RemoveAsync("lista_clientes");
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
                await _servicoCliente.DeletarAsync(id);
                await _cache.RemoveAsync("lista_clientes");
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Ocorreu um erro interno. Por favor, tente novamente mais tarde." });
            }
        }
    }
}