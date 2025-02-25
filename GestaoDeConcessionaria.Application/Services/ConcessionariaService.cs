using GestaoDeConcessionaria.Application.Interfaces;
using GestaoDeConcessionaria.Domain.Entities;
using GestaoDeConcessionaria.Domain.Interfaces;

namespace GestaoDeConcessionaria.Application.Services
{
    public class ConcessionariaService(IRepository<Concessionaria> repositorioConcessionaria) : IConcessionariaService
    {
        private readonly IRepository<Concessionaria> _repositorioConcessionaria = repositorioConcessionaria;

        public async Task<IEnumerable<Concessionaria>> ObterTodosAsync()
        {
            return await _repositorioConcessionaria.ObterTodosAsync();
        }

        public async Task<Concessionaria> ObterPorIdAsync(int id)
        {
            return await _repositorioConcessionaria.ObterPorIdAsync(id);
        }

        public async Task AdicionarAsync(Concessionaria concessionaria)
        {
            await _repositorioConcessionaria.AdicionarAsync(concessionaria);
            await _repositorioConcessionaria.SalvarAsync();
        }

        public async Task AtualizarAsync(Concessionaria concessionaria)
        {
            await _repositorioConcessionaria.AtualizarAsync(concessionaria);
            await _repositorioConcessionaria.SalvarAsync();
        }

        public async Task DeletarAsync(int id)
        {
            var concessionaria = await _repositorioConcessionaria.ObterPorIdAsync(id) ?? throw new Exception("Concessionária não encontrada.");
            concessionaria.Deletar();
            await _repositorioConcessionaria.AtualizarAsync(concessionaria);
            await _repositorioConcessionaria.SalvarAsync();
        }
    }
}
