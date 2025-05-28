using GestaoDeConcessionaria.Application.DTOs;
using MediatR;

namespace GestaoDeConcessionaria.Application.Queries.Vendas
{
    public record class BuscarDadosDeCriacaoDaVendaQuery() : IRequest<VendaDadosDeCriacaoDto>;
}
