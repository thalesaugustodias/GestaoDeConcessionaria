using GestaoDeConcessionaria.Application.DTOs;
using MediatR;

namespace GestaoDeConcessionaria.Application.Queries.Concessionarias
{
    public record BuscarTodasAsConcessionariasQuery() : IRequest<IEnumerable<ConcessionariaDto>>;
}
