using GestaoDeConcessionaria.Application.Commands.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeConcessionaria.API.Controllers
{
    [Route("api/[controller]"), ApiController]
    public class AuthController(IMediator med) : ControllerBase
    {
        private readonly IMediator _med = med;

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar(RegistrarUsuarioComando cmd)
        {
            await _med.Send(cmd);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUsuarioComando cmd)
        {
            var token = await _med.Send(cmd);
            return Ok(new { token });
        }
    }
}
