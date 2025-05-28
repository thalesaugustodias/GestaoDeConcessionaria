using GestaoDeConcessionaria.Application.DTOs;
using GestaoDeConcessionaria.Application.Factories;
using GestaoDeConcessionaria.Application.Interfaces;
using MediatR;

namespace GestaoDeConcessionaria.Application.Commands.Clientes
{
    public class CriarClienteHandler(IClienteService svc) : IRequestHandler<CriarClienteComando, ClienteDto>
    {
        private readonly IClienteService _svc = svc;

        public async Task<ClienteDto> Handle(CriarClienteComando request, CancellationToken cancellationToken)
        {
            var ent = ClienteFactory.Criar(request.Dto);
            await _svc.AdicionarAsync(ent);
            return new ClienteDto(ent.Id, ent.Nome, ent.CPF, ent.Telefone);
        }
    }
}
