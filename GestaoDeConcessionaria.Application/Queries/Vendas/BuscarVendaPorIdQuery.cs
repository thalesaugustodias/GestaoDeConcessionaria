using GestaoDeConcessionaria.Application.DTOs;
using MediatR;

namespace GestaoDeConcessionaria.Application.Queries.Vendas
{
    public record class BuscarVendaPorIdQuery(int Id) : IRequest<VendaDetalhesDto>;
}
