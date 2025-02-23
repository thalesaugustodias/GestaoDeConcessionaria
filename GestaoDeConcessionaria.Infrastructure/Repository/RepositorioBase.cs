using GestaoDeConcessionaria.Domain.Interfaces;
using GestaoDeConcessionaria.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GestaoDeConcessionaria.Infrastructure.Repository
{
    public class RepositorioBase<T> : IRepositorio<T> where T : class
    {
        protected readonly AplicationDbContext _contexto;
        protected readonly DbSet<T> _dbSet;

        public RepositorioBase(AplicationDbContext contexto)
        {
            _contexto = contexto;
            _dbSet = _contexto.Set<T>();
        }

        public async Task<IEnumerable<T>> ObterTodosAsync()
        {
            return await _dbSet.Where(e => EF.Property<bool>(e, "Ativo") == true).ToListAsync();
        }

        public async Task<T> ObterPorIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AdicionarAsync(T entidade)
        {
            await _dbSet.AddAsync(entidade);
        }

        public async Task AtualizarAsync(T entidade)
        {
            _dbSet.Update(entidade);
        }

        public async Task RemoverAsync(T entidade)
        {
            _dbSet.Remove(entidade);
        }

        public async Task SalvarAsync()
        {
            await _contexto.SaveChangesAsync();
        }
    }
}
