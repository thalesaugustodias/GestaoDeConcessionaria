using GestaoDeConcessionaria.Application.DTOs;
using MediatR;

namespace GestaoDeConcessionaria.Application.Queries.Veiculos
{
    public record BuscarTodosOsVeiculosQuery() : IRequest<IEnumerable<VeiculoDto>>;
}
