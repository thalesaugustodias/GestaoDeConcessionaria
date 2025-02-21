using GestaoDeConcessionaria.Domain.Entities;

namespace GestaoDeConcessionaria.Application.Interfaces
{
    public interface IConcessionariaService
    {
        Task<IEnumerable<Concessionaria>> ObterTodosAsync();
        Task<Concessionaria> ObterPorIdAsync(int id);
        Task AdicionarAsync(Concessionaria concessionaria);
        Task AtualizarAsync(Concessionaria concessionaria);
        Task DeletarAsync(int id);
    }
}
