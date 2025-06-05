using GestaoDeConcessionaria.Application.DTOs;
using MediatR;

namespace GestaoDeConcessionaria.Application.CQRS.Queries.Fabricantes
{
    public record BuscarFabricantePorIdQuery(int Id) : IRequest<FabricanteDto>;
}
