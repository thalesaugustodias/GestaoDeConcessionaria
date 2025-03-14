﻿using GestaoDeConcessionaria.Application.Interfaces;
using GestaoDeConcessionaria.Domain.Entities;
using GestaoDeConcessionaria.Domain.Interfaces;

namespace GestaoDeConcessionaria.Application.Services
{
    public class ClienteService(IClienteRepository repositorioCliente) : IClienteService
    {
        private readonly IClienteRepository _repositorioCliente = repositorioCliente;

        public async Task<IEnumerable<Cliente>> ObterTodosAsync()
        {
            return await _repositorioCliente.ObterTodosAsync();
        }

        public async Task<Cliente> ObterPorIdAsync(int id)
        {
            return await _repositorioCliente.ObterPorIdAsync(id);
        }

        public async Task AdicionarAsync(Cliente cliente)
        {
            var clienteExistente = await _repositorioCliente.CPFJaCadastradoAsync(cliente.CPF);
            if (clienteExistente)
            {
                throw new ArgumentException("Já existe um cliente cadastrado com esse CPF.");
            }

            await _repositorioCliente.AdicionarAsync(cliente);
            await _repositorioCliente.SalvarAsync();
        }

        public async Task AtualizarAsync(Cliente cliente)
        {
            await _repositorioCliente.AtualizarAsync(cliente);
            await _repositorioCliente.SalvarAsync();
        }

        public async Task DeletarAsync(int id)
        {
            var cliente = await _repositorioCliente.ObterPorIdAsync(id) ?? throw new Exception("Cliente não encontrado.");
            cliente.Deletar();
            await _repositorioCliente.AtualizarAsync(cliente);
            await _repositorioCliente.SalvarAsync();
        }
    }
}
