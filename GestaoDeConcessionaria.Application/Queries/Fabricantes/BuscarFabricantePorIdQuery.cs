using GestaoDeConcessionaria.Application.DTOs;
using MediatR;

namespace GestaoDeConcessionaria.Application.Queries.Fabricantes
{
    public record BuscarFabricantePorIdQuery(int Id) : IRequest<FabricanteDto>;
}
