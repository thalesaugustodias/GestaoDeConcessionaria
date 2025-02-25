using GestaoDeConcessionaria.Domain.Entities;
using GestaoDeConcessionaria.Domain.Interfaces;
using GestaoDeConcessionaria.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GestaoDeConcessionaria.Infrastructure.Repository
{
    public class ClienteRepository(AplicationDbContext contexto) : BaseRepository<Cliente>(contexto), IClienteRepository
    {
        public async Task<bool> CPFJaCadastradoAsync(string cpf)
        {
            return await _dbSet.AnyAsync(c => c.CPF == cpf);
        }
    }
}