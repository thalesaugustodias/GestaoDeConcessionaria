using GestaoDeConcessionaria.Application.DTOs;
using MediatR;

namespace GestaoDeConcessionaria.Application.CQRS.Queries.Vendas
{
    public record class BuscarTodasAsVendasQuery() : IRequest<IEnumerable<VendaDetalhesDto>>;
}
