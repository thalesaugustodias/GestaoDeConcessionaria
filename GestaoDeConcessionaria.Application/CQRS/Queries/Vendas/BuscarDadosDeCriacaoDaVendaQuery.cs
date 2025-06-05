using GestaoDeConcessionaria.Application.DTOs;
using MediatR;

namespace GestaoDeConcessionaria.Application.CQRS.Queries.Vendas
{
    public record class BuscarDadosDeCriacaoDaVendaQuery() : IRequest<VendaDadosDeCriacaoDto>;
}
