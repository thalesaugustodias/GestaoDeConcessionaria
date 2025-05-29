using GestaoDeConcessionaria.Application.Commands.Clientes;
using GestaoDeConcessionaria.Application.Queries.Clientes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeConcessionaria.API.Controllers
{
    [Route("api/[controller]"), ApiController]
    [Authorize(Roles = "Administrador,Vendedor,Gerente")]
    public class ClientesController(IMediator med) : ControllerBase
    {
        private readonly IMediator _med = med;

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> BuscarTodos() =>
            Ok(await _med.Send(new BuscarTodosOsClientesQuery()));

        [HttpGet("{id}"), AllowAnonymous]
        public async Task<IActionResult> BuscarPorId(int id) =>
            Ok(await _med.Send(new BuscarClientePorIdQuery(id)));

        [HttpPost]
        public async Task<IActionResult> Criar(CriarClienteComando cmd)
        {
            var dto = await _med.Send(cmd);
            return CreatedAtAction(nameof(BuscarPorId), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, AtualizarClienteComando cmd)
        {
            if (id != cmd.Dto.Id) return BadRequest();
            await _med.Send(cmd);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            await _med.Send(new DeletarClienteComando(id));
            return NoContent();
        }
    }
}