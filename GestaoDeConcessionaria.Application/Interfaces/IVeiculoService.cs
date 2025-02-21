using GestaoDeConcessionaria.Domain.Entities;

namespace GestaoDeConcessionaria.Application.Interfaces
{
    public interface IVeiculoService
    {
        Task<IEnumerable<Veiculo>> ObterTodosAsync();
        Task<Veiculo> ObterPorIdAsync(int id);
        Task AdicionarAsync(Veiculo veiculo);
        Task AtualizarAsync(Veiculo veiculo);
        Task DeletarAsync(int id);
    }
}
