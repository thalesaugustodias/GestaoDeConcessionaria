using GestaoDeConcessionaria.Application.DTOs;
using MediatR;

namespace GestaoDeConcessionaria.Application.CQRS.Commands.Vendas
{
    public record class CriarVendasComando(VendaDto Dto) : IRequest<VendaDetalhesDto>;
}
