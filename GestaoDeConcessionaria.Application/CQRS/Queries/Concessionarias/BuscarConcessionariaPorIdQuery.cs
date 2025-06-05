using GestaoDeConcessionaria.Application.DTOs;
using MediatR;

namespace GestaoDeConcessionaria.Application.CQRS.Queries.Concessionarias
{
    public record BuscarConcessionariaPorIdQuery(int Id) : IRequest<ConcessionariaDto>;
}
