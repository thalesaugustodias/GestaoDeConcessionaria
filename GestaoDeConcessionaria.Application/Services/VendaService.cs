using GestaoDeConcessionaria.Application.Interfaces;
using GestaoDeConcessionaria.Domain.Entities;
using GestaoDeConcessionaria.Domain.Interfaces;

namespace GestaoDeConcessionaria.Application.Services
{
    public class VendaService(IVendaRepository repositorioVenda) : IVendaService
    {
        private readonly IVendaRepository _repositorioVenda = repositorioVenda;

        public Venda? ObterPorIdAsync(int id)
        {
            return _repositorioVenda.ObterVendasPorIdAsync(id);
        }

        public async Task AdicionarAsync(Venda venda)
        {
            await _repositorioVenda.AdicionarAsync(venda);
            await _repositorioVenda.SalvarAsync();
        }

        public async Task<IEnumerable<Venda>> ObterTodasAsVendasAsync()
        {
            return await _repositorioVenda.ObterTodasAsVendasAsync();
        }
    }
}
