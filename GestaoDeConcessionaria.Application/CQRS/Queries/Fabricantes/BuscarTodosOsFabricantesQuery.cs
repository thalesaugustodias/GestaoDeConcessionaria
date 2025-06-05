using GestaoDeConcessionaria.Application.DTOs;
using MediatR;

namespace GestaoDeConcessionaria.Application.CQRS.Queries.Fabricantes
{
    public record BuscarTodosOsFabricantesQuery() : IRequest<IEnumerable<FabricanteDto>>;
}
