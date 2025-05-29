using GestaoDeConcessionaria.Application.DTOs;
using MediatR;

namespace GestaoDeConcessionaria.Application.Commands.Veiculos
{
    public record CriarVeiculosComando(VeiculoDto Dto) : IRequest<VeiculoDto>;
}
