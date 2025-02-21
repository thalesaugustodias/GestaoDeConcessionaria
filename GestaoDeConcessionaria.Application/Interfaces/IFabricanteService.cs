using GestaoDeConcessionaria.Domain.Entities;

namespace GestaoDeConcessionaria.Application.Interfaces
{
    public interface IFabricanteService
    {
        Task<IEnumerable<Fabricante>> ObterTodosAsync();
        Task<Fabricante> ObterPorIdAsync(int id);
        Task AdicionarAsync(Fabricante fabricante);
        Task AtualizarAsync(Fabricante fabricante);
        Task DeletarAsync(int id);
    }
}
