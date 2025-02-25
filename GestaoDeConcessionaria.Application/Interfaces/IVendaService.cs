using GestaoDeConcessionaria.Domain.Entities;

namespace GestaoDeConcessionaria.Application.Interfaces
{
    public interface IVendaService
    {
        Task<IEnumerable<Venda>> ObterTodosAsync();
        Task<Venda> ObterPorIdAsync(int id);
        Task AdicionarAsync(Venda venda);
        Task<IEnumerable<Venda>> ObterTodasAsVendasAsync();
    }
}
