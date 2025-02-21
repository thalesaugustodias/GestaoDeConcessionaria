using GestaoDeConcessionaria.Application.Interfaces;
using GestaoDeConcessionaria.Domain.Entities;
using GestaoDeConcessionaria.Domain.Interfaces;

namespace GestaoDeConcessionaria.Application.Services
{
    public class VendaService(IRepositorio<Venda> repositorioVenda) : IVendaService
    {
        private readonly IRepositorio<Venda> _repositorioVenda = repositorioVenda;

        public async Task<IEnumerable<Venda>> ObterTodosAsync()
        {
            return await _repositorioVenda.ObterTodosAsync();
        }

        public async Task<Venda> ObterPorIdAsync(int id)
        {
            return await _repositorioVenda.ObterPorIdAsync(id);
        }

        public async Task AdicionarAsync(Venda venda)
        {
            await _repositorioVenda.AdicionarAsync(venda);
            await _repositorioVenda.SalvarAsync();
        }
    }
}
