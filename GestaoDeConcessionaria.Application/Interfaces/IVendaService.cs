using GestaoDeConcessionaria.Domain.Entities;

namespace GestaoDeConcessionaria.Application.Interfaces
{
    public interface IVendaService
    {
        Venda? ObterPorIdAsync(int id);
        Task AdicionarAsync(Venda venda);
        Task<IEnumerable<Venda>> ObterTodasAsVendasAsync();
    }
}
