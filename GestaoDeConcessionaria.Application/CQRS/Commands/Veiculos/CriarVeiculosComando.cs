using GestaoDeConcessionaria.Application.DTOs;
using MediatR;

namespace GestaoDeConcessionaria.Application.CQRS.Commands.Veiculos
{
    public record CriarVeiculosComando(VeiculoDto Dto) : IRequest<VeiculoDto>;
}
