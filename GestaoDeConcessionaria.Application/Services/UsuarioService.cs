using GestaoDeConcessionaria.Application.Interfaces;
using GestaoDeConcessionaria.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using GestaoDeConcessionaria.Application;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GestaoDeConcessionaria.Application.Services
{
    public class UsuarioService(UserManager<Usuario> userManager, IOptions<JwtSettings> jwtOptions) : IUsuarioService
    {
        private readonly UserManager<Usuario> _userManager = userManager;
        private readonly JwtSettings _jwt = jwtOptions.Value;

        public async Task<Usuario> RegistrarAsync(Usuario usuario, string senha)
        {
            var resultado = await _userManager.CreateAsync(usuario, senha);
            if (!resultado.Succeeded)
                throw new Exception("Erro ao registrar usuário.");
            return usuario;
        }

        public async Task<string> AutenticarAsync(string nomeUsuario, string senha)
        {
            var usuario = await _userManager.FindByNameAsync(nomeUsuario);
            if (usuario == null || !await _userManager.CheckPasswordAsync(usuario, senha))
                throw new Exception("Credenciais inválidas.");

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Id),
                new Claim("nomeUsuario", usuario.UserName  ?? ""),
                new Claim("role", usuario.NivelAcesso.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Chave));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _jwt.Emissor,
                audience: _jwt.Publico,
                claims: claims,
                expires: DateTime.Now.AddHours(_jwt.ExpiracaoHoras),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
