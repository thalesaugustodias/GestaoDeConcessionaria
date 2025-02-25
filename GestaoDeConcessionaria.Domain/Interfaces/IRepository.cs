namespace GestaoDeConcessionaria.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> ObterTodosAsync();
        Task<T> ObterPorIdAsync(int id);
        Task AdicionarAsync(T entidade);
        Task AtualizarAsync(T entidade);
        Task RemoverAsync(T entidade);
        Task SalvarAsync();
    }
}
