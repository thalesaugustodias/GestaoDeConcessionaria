using GestaoDeConcessionaria.Application.DTOs;
using MediatR;

namespace GestaoDeConcessionaria.Application.CQRS.Commands.Veiculos
{
    public record AtualizarVeiculoComando(VeiculoDto Dto) : IRequest<Unit>;
}
