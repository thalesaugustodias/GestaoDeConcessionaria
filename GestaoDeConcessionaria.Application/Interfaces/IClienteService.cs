using GestaoDeConcessionaria.Domain.Entities;

namespace GestaoDeConcessionaria.Application.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<Cliente>> ObterTodosAsync();
        Task<Cliente> ObterPorIdAsync(int id);
        Task AdicionarAsync(Cliente cliente);
        Task AtualizarAsync(Cliente cliente);
        Task DeletarAsync(int id);
    }
}
