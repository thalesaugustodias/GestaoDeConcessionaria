using GestaoDeConcessionaria.Application.DTOs;
using MediatR;

namespace GestaoDeConcessionaria.Application.Queries.Fabricantes
{
    public record BuscarTodosOsFabricantesQuery() : IRequest<IEnumerable<FabricanteDto>>;
}
