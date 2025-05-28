using GestaoDeConcessionaria.Application.Commands.Vendas;
using GestaoDeConcessionaria.Application.Queries.Vendas;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeConcessionaria.API.Controllers
{
    [Route("api/[controller]"), ApiController]
    [Authorize(Roles = "Vendedor")]
    public class VendasController : ControllerBase
    {
        private readonly IMediator _med;
        public VendasController(IMediator med) => _med = med;

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> BuscarTodasAsVendas() =>
            Ok(await _med.Send(new BuscarTodasAsVendasQuery()));

        [HttpGet("{id}"), AllowAnonymous]
        public async Task<IActionResult> BuscarVendasPorId(int id) =>
            Ok(await _med.Send(new BuscarVendaPorIdQuery(id)));

        [HttpGet("obter-dados-para-criacao"), AllowAnonymous]
        public async Task<IActionResult> BuscarDadosDeCriacaoDaVenda() =>
            Ok(await _med.Send(new BuscarDadosDeCriacaoDaVendaQuery()));

        [HttpPost]
        public async Task<IActionResult> Criar(CriarVendasComando cmd)
        {
            var dto = await _med.Send(cmd);
            return CreatedAtAction(nameof(BuscarVendasPorId), new { id = dto.Id }, dto);
        }
    }
}