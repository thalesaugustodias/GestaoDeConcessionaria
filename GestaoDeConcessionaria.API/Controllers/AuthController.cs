using GestaoDeConcessionaria.Application.DTOs;
using GestaoDeConcessionaria.Application.Interfaces;
using GestaoDeConcessionaria.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeConcessionaria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IUsuarioService usuarioServico) : ControllerBase
    {
        private readonly IUsuarioService _usuarioServico = usuarioServico;

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] RegistrarUsuarioDto request)
        {
            var usuario = new Usuario
            {
                UserName = request.NomeUsuario,
                Email = request.Email,
                NivelAcesso = request.NivelAcesso
            };

            var resultado = await _usuarioServico.RegistrarAsync(usuario, request.Senha);
            return Ok(resultado);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUsuarioDto request)
        {
            var token = await _usuarioServico.AutenticarAsync(request.NomeUsuario, request.Senha);
            return Ok(new { token });
        }
    }
}
