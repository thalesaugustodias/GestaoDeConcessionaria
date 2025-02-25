using GestaoDeConcessionaria.Application.Interfaces;
using GestaoDeConcessionaria.Domain.Entities;
using GestaoDeConcessionaria.Domain.Interfaces;

namespace GestaoDeConcessionaria.Application.Services
{
    public class FabricanteService(IRepository<Fabricante> repositorioFabricante) : IFabricanteService
    {
        private readonly IRepository<Fabricante> _repositorioFabricante = repositorioFabricante;

        public async Task<IEnumerable<Fabricante>> ObterTodosAsync()
        {
            return await _repositorioFabricante.ObterTodosAsync();
        }

        public async Task<Fabricante> ObterPorIdAsync(int id)
        {
            return await _repositorioFabricante.ObterPorIdAsync(id);
        }

        public async Task AdicionarAsync(Fabricante fabricante)
        {
            await _repositorioFabricante.AdicionarAsync(fabricante);
            await _repositorioFabricante.SalvarAsync();
        }

        public async Task AtualizarAsync(Fabricante fabricante)
        {
            await _repositorioFabricante.AtualizarAsync(fabricante);
            await _repositorioFabricante.SalvarAsync();
        }

        public async Task DeletarAsync(int id)
        {
            var fabricante = await _repositorioFabricante.ObterPorIdAsync(id) ?? throw new Exception("Fabricante não encontrado.");
            fabricante.Deletar();
            await _repositorioFabricante.AtualizarAsync(fabricante);
            await _repositorioFabricante.SalvarAsync();
        }
    }
}
