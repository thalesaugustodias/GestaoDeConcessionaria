using GestaoDeConcessionaria.Domain.Entities;

namespace GestaoDeConcessionaria.Domain.Interfaces
{
    public interface IVeiculoRepository : IRepository<Veiculo>
    {
        Task<IEnumerable<Veiculo>> ObterTodosOsVeiculosAsync();
        Task<Veiculo?> ObterVeiculosPorIdAsync(int id);
    }
}
