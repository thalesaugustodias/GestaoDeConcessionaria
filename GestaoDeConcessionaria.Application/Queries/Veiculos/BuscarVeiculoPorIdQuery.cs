using GestaoDeConcessionaria.Application.DTOs;
using MediatR;

namespace GestaoDeConcessionaria.Application.Queries.Veiculos
{
    public record BuscarVeiculoPorIdQuery(int Id) : IRequest<VeiculoDto>;
}
