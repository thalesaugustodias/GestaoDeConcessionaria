using GestaoDeConcessionaria.Application.DTOs;
using MediatR;

namespace GestaoDeConcessionaria.Application.Queries.Concessionarias
{
    public record BuscarConcessionariaPorIdQuery(int Id) : IRequest<ConcessionariaDto>;
}
