using GestaoDeConcessionaria.Application.DTOs;
using GestaoDeConcessionaria.Application.Interfaces;
using MediatR;

namespace GestaoDeConcessionaria.Application.Queries.Concessionarias
{
    public class BuscarTodasAsConcessionariasHandler(IConcessionariaService svc) : IRequestHandler<BuscarTodasAsConcessionariasQuery, IEnumerable<ConcessionariaDto>>
    {
        private readonly IConcessionariaService _svc = svc;

        public async Task<IEnumerable<ConcessionariaDto>> Handle(BuscarTodasAsConcessionariasQuery q, CancellationToken ct)
        {
            var list = await _svc.ObterTodosAsync();
            return list.Select(c =>
                new ConcessionariaDto(
                  c.Id, c.Nome, c.Rua, c.Cidade,
                  c.Estado, c.CEP, c.Telefone,
                  c.Email, c.CapacidadeMaximaVeiculos
                )
            );
        }
    }
}
