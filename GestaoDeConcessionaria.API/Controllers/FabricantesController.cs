using GestaoDeConcessionaria.Application.Commands.Fabricantes;
using GestaoDeConcessionaria.Application.CQRS.Commands.Fabricantes;
using GestaoDeConcessionaria.Application.Queries.Fabricantes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeConcessionaria.API.Controllers
{
    [Route("api/[controller]"), ApiController]
    [Authorize(Roles = "Administrador")]
    public class FabricantesController : ControllerBase
    {
        private readonly IMediator _med;
        public FabricantesController(IMediator med) => _med = med;

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> BuscarTodos() =>
            Ok(await _med.Send(new BuscarTodosOsFabricantesQuery()));

        [HttpGet("{id}"), AllowAnonymous]
        public async Task<IActionResult> BuscarPorId(int id) =>
            Ok(await _med.Send(new BuscarFabricantePorIdQuery(id)));

        [HttpPost]
        public async Task<IActionResult> Criar(CriarFabricantesComando cmd)
        {
            var dto = await _med.Send(cmd);
            return CreatedAtAction(nameof(BuscarPorId), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, AtualizarFabricanteComando cmd)
        {
            if (id != cmd.Id) return BadRequest();
            await _med.Send(cmd);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            await _med.Send(new DeletarFabricanteComando(id));
            return NoContent();
        }
    }
}