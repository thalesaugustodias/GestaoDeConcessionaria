using GestaoDeConcessionaria.Application.Factories;
using GestaoDeConcessionaria.Application.Interfaces;
using MediatR;

namespace GestaoDeConcessionaria.Application.Commands.Concessionarias
{
    public class AtualizarConcessionariaHandler(IConcessionariaService svc) : IRequestHandler<AtualizarConcessionariaComando, Unit>
    {
        private readonly IConcessionariaService _svc = svc;

        public async Task<Unit> Handle(AtualizarConcessionariaComando cmd, CancellationToken ct)
        {
            var ent = await _svc.ObterPorIdAsync(cmd.Dto.Id)
                ?? throw new KeyNotFoundException("Concessionária não encontrada");
            ConcessionariaFactory.Atualizar(ent, cmd.Dto);
            await _svc.AtualizarAsync(ent);
            return Unit.Value;
        }
    }
}
