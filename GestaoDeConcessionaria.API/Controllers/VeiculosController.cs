using GestaoDeConcessionaria.Application.Commands.Veiculos;
using GestaoDeConcessionaria.Application.CQRS.Commands.Veiculos;
using GestaoDeConcessionaria.Application.Queries.Veiculos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeConcessionaria.API.Controllers
{
    [Route("api/[controller]"), ApiController]
    [Authorize(Roles = "Gerente,Administrador")]
    public class VeiculosController(IMediator med) : ControllerBase
    {
        private readonly IMediator _med = med;

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> BuscarTodos() =>
            Ok(await _med.Send(new BuscarTodosOsVeiculosQuery()));

        [HttpGet("{id}"), AllowAnonymous]
        public async Task<IActionResult> BuscarPorId(int id) =>
            Ok(await _med.Send(new BuscarVeiculoPorIdQuery(id)));

        [HttpPost]
        public async Task<IActionResult> Criar(CriarVeiculosComando cmd)
        {
            var dto = await _med.Send(cmd);
            return CreatedAtAction(nameof(BuscarPorId), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, AtualizarVeiculoComando cmd)
        {
            if (id != cmd.Id) return BadRequest();
            await _med.Send(cmd);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            await _med.Send(new DeletarVeiculoComando(id));
            return NoContent();
        }
    }
}