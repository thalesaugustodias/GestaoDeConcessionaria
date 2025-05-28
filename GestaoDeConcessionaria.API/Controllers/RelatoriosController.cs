using GestaoDeConcessionaria.Application.Queries.Relatorios;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeConcessionaria.API.Controllers
{
    [Route("api/[controller]"), ApiController]
    [Authorize(Roles = "Administrador,Gerente,Vendedor")]
    public class RelatoriosController(IMediator med) : ControllerBase
    {
        private readonly IMediator _med = med;

        [HttpGet("mensal")]
        public async Task<IActionResult> Mensal([FromQuery] BuscarRelatorioMensalQuery q)
            => Ok(await _med.Send(q));
    }
}