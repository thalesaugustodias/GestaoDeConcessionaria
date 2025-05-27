using GestaoDeConcessionaria.Domain.Entities;
using GestaoDeConcessionaria.Domain.Interfaces;
using GestaoDeConcessionaria.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GestaoDeConcessionaria.Infrastructure.Repository
{
    public class VeiculoRepository(AplicationDbContext contexto) : BaseRepository<Veiculo>(contexto), IVeiculoRepository
    {

        public async Task<IEnumerable<Veiculo>> ObterTodosOsVeiculosAsync()
        {
            return await _contexto.Veiculos
                .AsNoTracking()
                .Include(v => v.Fabricante)
                .Where(v => EF.Property<bool>(v, "Ativo") == true)
                .ToListAsync();
        }

        public async Task<Veiculo?> ObterVeiculosPorIdAsync(int id)
        {
            return await _contexto.Veiculos
                .Include(v => v.Fabricante)
                .Where(v => EF.Property<bool>(v, "Ativo") == true && v.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
