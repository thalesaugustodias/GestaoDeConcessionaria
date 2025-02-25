using GestaoDeConcessionaria.Application.Interfaces;
using GestaoDeConcessionaria.Domain.Entities;
using GestaoDeConcessionaria.Domain.Interfaces;

namespace GestaoDeConcessionaria.Application.Services
{
    public class VeiculoService(IRepository<Veiculo> repositorioVeiculo) : IVeiculoService
    {
        private readonly IRepository<Veiculo> _repositorioVeiculo = repositorioVeiculo;

        public async Task<IEnumerable<Veiculo>> ObterTodosAsync()
        {
            return await _repositorioVeiculo.ObterTodosAsync();
        }

        public async Task<Veiculo> ObterPorIdAsync(int id)
        {
            return await _repositorioVeiculo.ObterPorIdAsync(id);
        }

        public async Task AdicionarAsync(Veiculo veiculo)
        {
            await _repositorioVeiculo.AdicionarAsync(veiculo);
            await _repositorioVeiculo.SalvarAsync();
        }

        public async Task AtualizarAsync(Veiculo veiculo)
        {
            await _repositorioVeiculo.AtualizarAsync(veiculo);
            await _repositorioVeiculo.SalvarAsync();
        }

        public async Task DeletarAsync(int id)
        {
            var veiculo = await _repositorioVeiculo.ObterPorIdAsync(id) ?? throw new Exception("Veículo não encontrado.");
            veiculo.Deletar();
            await _repositorioVeiculo.AtualizarAsync(veiculo);
            await _repositorioVeiculo.SalvarAsync();
        }
    }
}
