using GestaoDeConcessionaria.Application.DTOs;
using MediatR;

namespace GestaoDeConcessionaria.Application.Queries.Clientes
{
    public record BuscarTodosOsClientesQuery : IRequest<IEnumerable<ClienteDto>>;
}
