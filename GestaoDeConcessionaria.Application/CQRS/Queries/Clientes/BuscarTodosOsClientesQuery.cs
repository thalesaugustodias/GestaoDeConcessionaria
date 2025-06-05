using GestaoDeConcessionaria.Application.DTOs;
using MediatR;

namespace GestaoDeConcessionaria.Application.CQRS.Queries.Clientes
{
    public record BuscarTodosOsClientesQuery : IRequest<IEnumerable<ClienteDto>>;
}
