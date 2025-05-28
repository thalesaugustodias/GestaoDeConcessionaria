using GestaoDeConcessionaria.Application.DTOs;
using MediatR;

namespace GestaoDeConcessionaria.Application.Commands.Vendas
{
    public record class CriarVendasComando(VendaDto Dto) : IRequest<VendaDetalhesDto>;
}
