using GestaoDeConcessionaria.Application.CQRS.Commands.Notificacoes;
using GestaoDeConcessionaria.Application.CQRS.Queries.Notificacao;
using GestaoDeConcessionaria.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeConcessionaria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrador")]
    public class NotificacaoController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotificacaoDto>>> ObterTodas()
        {
            var result = await _mediator.Send(new BuscarTodasAsNotificacoesQuery());
            return Ok(result);
        }

        [HttpGet("por-concessionaria/{concessionariaId:int}")]
        public async Task<ActionResult<NotificacaoDto?>> BuscarConcessionariaPorId(int concessionariaId)
        {
            var result = await _mediator.Send(new BuscarNotificacaoPorConcessionariaQuery
            {
                ConcessionariaId = concessionariaId
            });

            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NotificacaoDto?>> BuscarPorId(string id)
        {
            var result = await _mediator.Send(new BuscarNotificacaoPorIdQuery { Id = id });
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<NotificacaoDto>> Criar([FromBody] CriarNotificacaoComando cmd)
        {
            var result = await _mediator.Send(cmd);
            return CreatedAtAction(nameof(BuscarPorId), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] AtualizarNotificacaoComando cmd)
        {
            if (id != cmd.Id)
                return BadRequest("Id na URL difere do Id no corpo.");

            await _mediator.Send(cmd);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _mediator.Send(new DeletarNotificacaoComando { Id = id });
            return NoContent();
        }
    }
}
