using GestaoDeConcessionaria.Application.DTOs;
using GestaoDeConcessionaria.Application.Interfaces;
using MediatR;

namespace GestaoDeConcessionaria.Application.Queries.Clientes
{
    public class BuscarClientePorIdHandler(IClienteService svc) : IRequestHandler<BuscarClientePorIdQuery, ClienteDto>
    {
        public async Task<ClienteDto> Handle(BuscarClientePorIdQuery q, CancellationToken ct)
        {
            var c = await svc.ObterPorIdAsync(q.Id)
                ?? throw new KeyNotFoundException("Cliente não encontrado");
            return new ClienteDto(c.Id, c.Nome, c.CPF, c.Telefone);
        }
    }
}
