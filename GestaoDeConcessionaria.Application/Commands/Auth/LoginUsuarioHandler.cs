using GestaoDeConcessionaria.Application.Interfaces;
using MediatR;

namespace GestaoDeConcessionaria.Application.Commands.Auth
{
    public class LoginUsuarioHandler(IUsuarioService svc) : IRequestHandler<LoginUsuarioComando, string>
    {
        private readonly IUsuarioService _svc = svc;

        public async Task<string> Handle(LoginUsuarioComando cmd, CancellationToken ct)
            => await _svc.AutenticarAsync(cmd.Dto.NomeUsuario, cmd.Dto.Senha);
    }
}
