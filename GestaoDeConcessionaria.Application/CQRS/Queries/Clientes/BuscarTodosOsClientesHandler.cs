using GestaoDeConcessionaria.Application.DTOs;
using GestaoDeConcessionaria.Application.Interfaces;
using MediatR;

namespace GestaoDeConcessionaria.Application.CQRS.Queries.Clientes
{
    public class BuscarTodosOsClientesHandler(IClienteService svc) : IRequestHandler<BuscarTodosOsClientesQuery, IEnumerable<ClienteDto>>
    {
        public async Task<IEnumerable<ClienteDto>> Handle(BuscarTodosOsClientesQuery q, CancellationToken ct)
        {
            var list = await svc.ObterTodosAsync();
            return list.Select(c => new ClienteDto(c.Id, c.Nome, c.CPF, c.Telefone));
        }
    }
}
