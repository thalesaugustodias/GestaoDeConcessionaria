using GestaoDeConcessionaria.Domain.Entities;
using GestaoDeConcessionaria.Domain.Interfaces;
using GestaoDeConcessionaria.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GestaoDeConcessionaria.Infrastructure.Repository
{
    public class VendaRepository(AplicationDbContext contexto) : BaseRepository<Venda>(contexto), IVendaRepository
    {

        public async Task<IEnumerable<Venda>> ObterTodasAsVendasAsync()
        {
            return await _contexto.Vendas
                .AsNoTracking()
                .Include(v => v.Veiculo)
                    .ThenInclude(v => v.Fabricante)
                .Include(v => v.Concessionaria)
                .Include(v => v.Cliente)
                .Where(v => EF.Property<bool>(v, "Ativo") == true)
                .ToListAsync();
        }
    }
}
