using GestaoDeConcessionaria.Application.DTOs;
using MediatR;

namespace GestaoDeConcessionaria.Application.CQRS.Queries.Veiculos
{
    public record BuscarVeiculoPorIdQuery(int Id) : IRequest<VeiculoDto>;
}
