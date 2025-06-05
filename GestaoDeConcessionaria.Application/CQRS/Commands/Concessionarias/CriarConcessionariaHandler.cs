using GestaoDeConcessionaria.Application.DTOs;
using GestaoDeConcessionaria.Application.Factories;
using GestaoDeConcessionaria.Application.Interfaces;
using MediatR;

namespace GestaoDeConcessionaria.Application.CQRS.Commands.Concessionarias
{
    public class CriarConcessionariaHandler(IConcessionariaService svc) : IRequestHandler<CriarConcessionariaComando, ConcessionariaDto>
    {
        private readonly IConcessionariaService _svc = svc;

        public async Task<ConcessionariaDto> Handle(CriarConcessionariaComando cmd, CancellationToken ct)
        {
            var ent = ConcessionariaFactory.Criar(cmd.Dto);
            await _svc.AdicionarAsync(ent);
            return new ConcessionariaDto(
                ent.Id, ent.Nome, ent.Rua, ent.Cidade,
                ent.Estado, ent.CEP, ent.Telefone,
                ent.Email, ent.CapacidadeMaximaVeiculos
            );
        }
    }
}
