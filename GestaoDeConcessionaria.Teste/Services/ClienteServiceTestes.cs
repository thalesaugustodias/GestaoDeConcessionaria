using GestaoDeConcessionaria.Application.Services;
using GestaoDeConcessionaria.Domain.Entities;
using GestaoDeConcessionaria.Domain.Interfaces;
using Moq;

namespace GestaoDeConcessionaria.Teste.Services
{
    public class ClienteServiceTestes
    {
        private readonly Mock<IClienteRepository> _clienteRepositoryMock;
        private readonly ClienteService _clienteService;

        public ClienteServiceTestes()
        {
            _clienteRepositoryMock = new Mock<IClienteRepository>();
            _clienteService = new ClienteService(_clienteRepositoryMock.Object);
        }

        [Fact]
        public async Task ObterTodosAsync_ReturnsAllClientes()
        {
            // Arrange
            var clientes = new List<Cliente>
            {
                new("João", "12345678901", "111111111"),
                new("Maria", "10987654321", "222222222")
            };

            _clienteRepositoryMock.Setup(r => r.ObterTodosAsync())
                .ReturnsAsync(clientes);

            // Act
            var result = await _clienteService.ObterTodosAsync();

            // Assert
            Assert.Equal(clientes, result);
            _clienteRepositoryMock.Verify(r => r.ObterTodosAsync(), Times.Once);
        }

        [Fact]
        public async Task ObterPorIdAsync_ReturnsCliente()
        {
            // Arrange
            var cliente = new Cliente("João", "12345678901", "111111111");
            _clienteRepositoryMock.Setup(r => r.ObterPorIdAsync(1))
                .ReturnsAsync(cliente);

            // Act
            var result = await _clienteService.ObterPorIdAsync(1);

            // Assert
            Assert.Equal(cliente, result);
            _clienteRepositoryMock.Verify(r => r.ObterPorIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task AdicionarAsync_ThrowsException_WhenCpfAlreadyExists()
        {
            // Arrange
            var cliente = new Cliente("João", "12345678901", "111111111");
            _clienteRepositoryMock.Setup(r => r.CPFJaCadastradoAsync(cliente.CPF))
                .ReturnsAsync(true);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _clienteService.AdicionarAsync(cliente));
            Assert.Equal("Já existe um cliente cadastrado com esse CPF.", exception.Message);
            _clienteRepositoryMock.Verify(r => r.AdicionarAsync(It.IsAny<Cliente>()), Times.Never);
        }

        [Fact]
        public async Task AdicionarAsync_AddsCliente_WhenCpfNotExists()
        {
            // Arrange
            var cliente = new Cliente("João", "12345678901", "111111111");
            _clienteRepositoryMock.Setup(r => r.CPFJaCadastradoAsync(cliente.CPF))
                .ReturnsAsync(false);
            _clienteRepositoryMock.Setup(r => r.AdicionarAsync(cliente)).Returns(Task.CompletedTask);
            _clienteRepositoryMock.Setup(r => r.SalvarAsync()).Returns(Task.CompletedTask);

            // Act
            await _clienteService.AdicionarAsync(cliente);

            // Assert
            _clienteRepositoryMock.Verify(r => r.CPFJaCadastradoAsync(cliente.CPF), Times.Once);
            _clienteRepositoryMock.Verify(r => r.AdicionarAsync(cliente), Times.Once);
            _clienteRepositoryMock.Verify(r => r.SalvarAsync(), Times.Once);
        }

        [Fact]
        public async Task AtualizarAsync_UpdatesCliente()
        {
            // Arrange
            var cliente = new Cliente("João", "12345678901", "111111111");
            _clienteRepositoryMock.Setup(r => r.AtualizarAsync(cliente)).Returns(Task.CompletedTask);
            _clienteRepositoryMock.Setup(r => r.SalvarAsync()).Returns(Task.CompletedTask);

            // Act
            await _clienteService.AtualizarAsync(cliente);

            // Assert
            _clienteRepositoryMock.Verify(r => r.AtualizarAsync(cliente), Times.Once);
            _clienteRepositoryMock.Verify(r => r.SalvarAsync(), Times.Once);
        }

        [Fact]
        public async Task DeletarAsync_ThrowsException_WhenClienteNotFound()
        {
            // Arrange
            _clienteRepositoryMock.Setup(r => r.ObterPorIdAsync(1))
                .ReturnsAsync((Cliente?)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _clienteService.DeletarAsync(1));
            Assert.Equal("Cliente não encontrado.", exception.Message);
            _clienteRepositoryMock.Verify(r => r.ObterPorIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task DeletarAsync_DeletesCliente_WhenFound()
        {
            // Arrange
            var cliente = new Cliente("João", "12345678901", "111111111");
            _clienteRepositoryMock.Setup(r => r.ObterPorIdAsync(1))
                .ReturnsAsync(cliente);
            _clienteRepositoryMock.Setup(r => r.AtualizarAsync(cliente)).Returns(Task.CompletedTask);
            _clienteRepositoryMock.Setup(r => r.SalvarAsync()).Returns(Task.CompletedTask);

            // Act
            await _clienteService.DeletarAsync(1);

            // Assert
            _clienteRepositoryMock.Verify(r => r.ObterPorIdAsync(1), Times.Once);
            _clienteRepositoryMock.Verify(r => r.AtualizarAsync(cliente), Times.Once);
            _clienteRepositoryMock.Verify(r => r.SalvarAsync(), Times.Once);
        }
    }
}