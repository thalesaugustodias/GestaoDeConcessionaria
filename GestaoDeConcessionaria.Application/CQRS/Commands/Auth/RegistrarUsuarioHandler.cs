using GestaoDeConcessionaria.Application.Interfaces;
using MediatR;

namespace GestaoDeConcessionaria.Application.CQRS.Commands.Auth
{
    public class RegistrarUsuarioHandler(IUsuarioService svc) : IRequestHandler<RegistrarUsuarioComando, Unit>
    {
        private readonly IUsuarioService _svc = svc;

        public async Task<Unit> Handle(RegistrarUsuarioComando request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;
            var user = new Domain.Entities.Usuario
            {
                UserName = dto?.NomeUsuario,
                Email = dto?.Email,
                NivelAcesso = dto.NivelAcesso
            };
            await _svc.RegistrarAsync(user, dto.Senha);
            return Unit.Value;
        }
    }
}
