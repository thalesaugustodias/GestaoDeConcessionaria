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
    [Authorize(Roles = "Vendedor")]
    public class VendasController(IVendaService servicoVenda, IVeiculoService servicoVeiculo,
                            IConcessionariaService servicoConcessionaria, IClienteService servicoCliente,
                            IDistributedCache cache) : ControllerBase
    {
        private readonly IVendaService _servicoVenda = servicoVenda;
        private readonly IVeiculoService _servicoVeiculo = servicoVeiculo;
        private readonly IConcessionariaService _servicoConcessionaria = servicoConcessionaria;
        private readonly IClienteService _servicoCliente = servicoCliente;
        private readonly IDistributedCache _cache = cache;

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ObterTodos()
        {
            const string cacheKey = "lista_vendas";
            string? jsonLista = await _cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(jsonLista))
            {
                var vendasCache = JsonSerializer.Deserialize<IEnumerable<Venda>>(jsonLista);
                return Ok(vendasCache);
            }

            var vendas = await _servicoVenda.ObterTodasAsVendasAsync();
            jsonLista = JsonSerializer.Serialize(vendas);
            var options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5));
            await _cache.SetStringAsync(cacheKey, jsonLista, options);
            return Ok(vendas);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult ObterPorId(int id)
        {
            var venda = _servicoVenda.ObterPorIdAsync(id);
            if (venda == null)
                return NotFound();

            var dto = VendaFactory.CreateDetalhes(venda);
            return Ok(dto);
        }

        [HttpGet("obter-dados-para-criacao")]
        [AllowAnonymous]
        public async Task<IActionResult> ObterDadosParaCriacao()
        {
            var veiculos = await _servicoVeiculo.ObterTodosAsync();
            var concessionarias = await _servicoConcessionaria.ObterTodosAsync();
            var clientes = await _servicoCliente.ObterTodosAsync();

            var veiculosDto = VeiculoFactory.CreateList(veiculos);
            var concessionariasDto = ConcessionariaFactory.CriacaoDeConcessionariaDto(concessionarias);
            var clientesDto = ClienteFactory.CriarClienteDto(clientes);

            var dados = VendaFactory.DadosDeCriacao(veiculosDto, concessionariasDto, clientesDto);

            return Ok(dados);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] VendaDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var veiculo = await _servicoVeiculo.ObterPorIdAsync(dto.VeiculoId);
            if (veiculo == null)
                return BadRequest(new { Message = "Veículo não encontrado." });

            var concessionaria = await _servicoConcessionaria.ObterPorIdAsync(dto.ConcessionariaId);
            if (concessionaria == null)
                return BadRequest(new { Message = "Concessionária não encontrada." });

            var cliente = await _servicoCliente.ObterPorIdAsync(dto.ClienteId);
            if (cliente == null)
                return BadRequest(new { Message = "Cliente não encontrado." });

            try
            {
                var venda = VendaFactory.Criar(dto, veiculo, concessionaria, cliente);
                await _servicoVenda.AdicionarAsync(venda);
                await _cache.RemoveAsync("lista_vendas");
                return CreatedAtAction(nameof(ObterPorId), new { id = venda.Id }, venda);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Erro ao adicionar venda: " + ex.Message });
            }
        }
    }
}