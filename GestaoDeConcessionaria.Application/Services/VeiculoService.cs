using GestaoDeConcessionaria.Application.Interfaces;
using GestaoDeConcessionaria.Domain.Entities;
using GestaoDeConcessionaria.Domain.Interfaces;

namespace GestaoDeConcessionaria.Application.Services
{
    public class VeiculoService(IVeiculoRepository repositorioVeiculo, IRepository<Veiculo> repository) : IVeiculoService
    {
        private readonly IRepository<Veiculo> _repository = repository;
        private readonly IVeiculoRepository _repositorioVeiculo = repositorioVeiculo;

        public async Task<IEnumerable<Veiculo>> ObterTodosAsync()
        {
            return await _repositorioVeiculo.ObterTodosOsVeiculosAsync();
        }

        public async Task<Veiculo> ObterPorIdAsync(int id)
        {
            var veiculo = await _repositorioVeiculo.ObterVeiculosPorIdAsync(id);
            return veiculo ?? throw new ArgumentNullException(nameof(id), "Veículo não encontrado.");
        }

        public async Task AdicionarAsync(Veiculo veiculo)
        {
            await _repository.AdicionarAsync(veiculo);
            await _repository.SalvarAsync();
        }

        public async Task AtualizarAsync(Veiculo veiculo)
        {
            await _repository.AtualizarAsync(veiculo);
            await _repository.SalvarAsync();
        }

        public async Task DeletarAsync(int id)
        {
            var veiculo = await _repository.ObterPorIdAsync(id) ?? throw new Exception("Veículo não encontrado.");
            veiculo.Deletar();
            await _repository.AtualizarAsync(veiculo);
            await _repository.SalvarAsync();
        }
    }
}
