﻿using GestaoDeConcessionaria.Application.DTOs;
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
    [Authorize(Roles = "Vendedor")]
    public class VendasController : ControllerBase
    {
        private readonly IVendaService _servicoVenda;
        private readonly IVeiculoService _servicoVeiculo;
        private readonly IConcessionariaService _servicoConcessionaria;
        private readonly IClienteService _servicoCliente;
        private readonly IDistributedCache _cache;

        public VendasController(IVendaService servicoVenda, IVeiculoService servicoVeiculo,
                                IConcessionariaService servicoConcessionaria, IClienteService servicoCliente,
                                IDistributedCache cache)
        {
            _servicoVenda = servicoVenda;
            _servicoVeiculo = servicoVeiculo;
            _servicoConcessionaria = servicoConcessionaria;
            _servicoCliente = servicoCliente;
            _cache = cache;
        }

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
        public async Task<IActionResult> Adicionar([FromBody] VendaDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var veiculo = await _servicoVeiculo.ObterPorIdAsync(dto.VeiculoId);
            if (veiculo == null)
                return BadRequest("Veículo não encontrado.");

            var concessionaria = await _servicoConcessionaria.ObterPorIdAsync(dto.ConcessionariaId);
            if (concessionaria == null)
                return BadRequest("Concessionária não encontrada.");

            var cliente = await _servicoCliente.ObterPorIdAsync(dto.ClienteId);
            if (cliente == null)
                return BadRequest("Cliente não encontrado.");

            var venda = VendaFactory.Criar(dto, veiculo, concessionaria, cliente);
            await _servicoVenda.AdicionarAsync(venda);
            await _cache.RemoveAsync("lista_vendas");
            return CreatedAtAction(nameof(ObterPorId), new { id = venda.Id }, venda);
        }
    }
}
