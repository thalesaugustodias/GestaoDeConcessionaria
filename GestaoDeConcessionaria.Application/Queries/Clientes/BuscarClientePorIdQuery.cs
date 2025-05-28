using GestaoDeConcessionaria.Application.DTOs;
using MediatR;

namespace GestaoDeConcessionaria.Application.Queries.Clientes
{
    public record BuscarClientePorIdQuery(int Id) : IRequest<ClienteDto>;
}
