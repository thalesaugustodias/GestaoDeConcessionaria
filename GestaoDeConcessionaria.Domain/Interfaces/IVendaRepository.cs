using GestaoDeConcessionaria.Domain.Entities;

namespace GestaoDeConcessionaria.Domain.Interfaces
{
    public interface IVendaRepository : IRepository<Venda>
    {
        Task<IEnumerable<Venda>> ObterTodasAsVendasAsync();
        Venda? ObterVendasPorIdAsync(int id);
    }
}