using GestaoDeConcessionaria.Application.DTOs;
using MediatR;

namespace GestaoDeConcessionaria.Application.CQRS.Queries.Clientes
{
    public record BuscarClientePorIdQuery(int Id) : IRequest<ClienteDto>;
}
