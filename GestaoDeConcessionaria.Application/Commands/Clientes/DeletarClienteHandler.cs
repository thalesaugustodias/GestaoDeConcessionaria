using GestaoDeConcessionaria.Application.Interfaces;
using MediatR;

namespace GestaoDeConcessionaria.Application.Commands.Clientes
{
    public class DeletarClienteHandler(IClienteService svc) : IRequestHandler<DeletarClienteComando, Unit>
    {
        private readonly IClienteService _svc = svc;

        public async Task<Unit> Handle(DeletarClienteComando cmd, CancellationToken ct)
        {
            await _svc.DeletarAsync(cmd.Id);
            return Unit.Value;
        }
    }
}
