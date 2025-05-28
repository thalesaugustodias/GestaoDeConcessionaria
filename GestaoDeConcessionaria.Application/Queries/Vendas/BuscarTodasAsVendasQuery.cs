using GestaoDeConcessionaria.Application.DTOs;
using MediatR;

namespace GestaoDeConcessionaria.Application.Queries.Vendas
{
    public record class BuscarTodasAsVendasQuery() : IRequest<IEnumerable<VendaDetalhesDto>>;
}
