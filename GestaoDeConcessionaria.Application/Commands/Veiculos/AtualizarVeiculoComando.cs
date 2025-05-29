using GestaoDeConcessionaria.Application.DTOs;
using MediatR;

namespace GestaoDeConcessionaria.Application.Commands.Veiculos
{
    public record AtualizarVeiculoComando(int Id, VeiculoDto Dto) : IRequest<Unit>;
}
