using GestaoDeConcessionaria.Domain.Entities;

namespace GestaoDeConcessionaria.Domain.Interfaces
{
    public interface IVeiculoRepository
    {
        Task<IEnumerable<Veiculo>> ObterTodosOsVeiculosAsync();
        Task<Veiculo?> ObterVeiculosPorIdAsync(int id);
    }
}
