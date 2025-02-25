using GestaoDeConcessionaria.Domain.Entities;

namespace GestaoDeConcessionaria.Domain.Interfaces
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<bool> CPFJaCadastradoAsync(string cpf);
    }
}