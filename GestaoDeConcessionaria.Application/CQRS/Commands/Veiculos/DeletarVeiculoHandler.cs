using GestaoDeConcessionaria.Application.Interfaces;
using MediatR;

namespace GestaoDeConcessionaria.Application.CQRS.Commands.Veiculos
{
    public class DeletarVeiculoHandler(IVeiculoService svc) : IRequestHandler<DeletarVeiculoComando, Unit>
    {
        private readonly IVeiculoService _svc = svc;

        public async Task<Unit> Handle(DeletarVeiculoComando cmd, CancellationToken ct)
        {
            await _svc.DeletarAsync(cmd.Id);
            return Unit.Value;
        }
    }
}
