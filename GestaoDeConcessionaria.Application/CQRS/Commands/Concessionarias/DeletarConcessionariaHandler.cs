using GestaoDeConcessionaria.Application.Interfaces;
using MediatR;

namespace GestaoDeConcessionaria.Application.CQRS.Commands.Concessionarias
{
    public class DeletarConcessionariaHandler(IConcessionariaService svc) : IRequestHandler<DeletarConcessionariaComando, Unit>
    {
        private readonly IConcessionariaService _svc = svc;

        public async Task<Unit> Handle(DeletarConcessionariaComando cmd, CancellationToken ct)
        {
            await _svc.DeletarAsync(cmd.Id);
            return Unit.Value;
        }
    }
}
