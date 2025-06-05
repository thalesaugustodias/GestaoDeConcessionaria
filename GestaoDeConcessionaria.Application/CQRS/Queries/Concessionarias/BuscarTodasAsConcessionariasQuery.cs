using GestaoDeConcessionaria.Application.DTOs;
using MediatR;

namespace GestaoDeConcessionaria.Application.CQRS.Queries.Concessionarias
{
    public record BuscarTodasAsConcessionariasQuery() : IRequest<IEnumerable<ConcessionariaDto>>;
}
