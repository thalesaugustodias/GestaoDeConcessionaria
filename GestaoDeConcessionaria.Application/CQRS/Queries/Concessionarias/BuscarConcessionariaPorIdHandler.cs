using GestaoDeConcessionaria.Application.DTOs;
using GestaoDeConcessionaria.Application.Interfaces;
using MediatR;

namespace GestaoDeConcessionaria.Application.CQRS.Queries.Concessionarias
{
    public class BuscarConcessionariaPorIdHandler(IConcessionariaService svc) : IRequestHandler<BuscarConcessionariaPorIdQuery, ConcessionariaDto>
    {
        private readonly IConcessionariaService _svc = svc;

        public async Task<ConcessionariaDto> Handle(BuscarConcessionariaPorIdQuery q, CancellationToken ct)
        {
            var c = await _svc.ObterPorIdAsync(q.Id)
                ?? throw new KeyNotFoundException("Concessionária não encontrada");
            return new ConcessionariaDto(
                c.Id, c.Nome, c.Rua, c.Cidade,
                c.Estado, c.CEP, c.Telefone,
                c.Email, c.CapacidadeMaximaVeiculos
            );
        }
    }
}
