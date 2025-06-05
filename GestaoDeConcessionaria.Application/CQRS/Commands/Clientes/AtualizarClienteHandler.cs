using GestaoDeConcessionaria.Application.Factories;
using GestaoDeConcessionaria.Application.Interfaces;
using MediatR;

namespace GestaoDeConcessionaria.Application.CQRS.Commands.Clientes
{
    public class AtualizarClienteHandler(IClienteService svc) : IRequestHandler<AtualizarClienteComando, Unit>
    {
        private readonly IClienteService _svc = svc;

        public async Task<Unit> Handle(AtualizarClienteComando request, CancellationToken cancellationToken)
        {
            var ent = await _svc.ObterPorIdAsync(request.Dto.Id)
            ?? throw new KeyNotFoundException("Cliente não encontrado");
            ClienteFactory.Atualizar(ent, request.Dto);
            await _svc.AtualizarAsync(ent);
            return Unit.Value;
        }
    }
}
