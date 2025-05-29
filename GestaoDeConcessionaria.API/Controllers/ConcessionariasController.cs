using GestaoDeConcessionaria.Application.Commands.Concessionarias;
using GestaoDeConcessionaria.Application.Queries.Concessionarias;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeConcessionaria.API.Controllers
{
    [Route("api/[controller]"), ApiController]
    [Authorize(Roles = "Administrador,Vendedor,Gerente")]
    public class ConcessionariasController(IMediator med) : ControllerBase
    {
        private readonly IMediator _med = med;

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> BuscarTodasAsConcessionarias() =>
            Ok(await _med.Send(new BuscarTodasAsConcessionariasQuery()));

        [HttpGet("{id}"), AllowAnonymous]
        public async Task<IActionResult> BuscarPorId(int id) =>
            Ok(await _med.Send(new BuscarConcessionariaPorIdQuery(id)));

        [HttpPost]
        public async Task<IActionResult> Criar(CriarConcessionariaComando cmd)
        {
            var dto = await _med.Send(cmd);
            return CreatedAtAction(nameof(BuscarPorId), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, AtualizarConcessionariaComando cmd)
        {
            if (id != cmd.Id) return BadRequest();
            await _med.Send(cmd);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _med.Send(new DeletarConcessionariaComando(id));
            return NoContent();
        }
    }
}