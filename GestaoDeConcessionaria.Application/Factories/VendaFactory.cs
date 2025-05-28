using GestaoDeConcessionaria.Application.DTOs;
using GestaoDeConcessionaria.Domain.Entities;

namespace GestaoDeConcessionaria.Application.Factories
{
    public static class VendaFactory
    {
        public static Venda Criar(VendaDto dto, Veiculo veiculo, Concessionaria concessionaria, Cliente cliente)
        {
            return new Venda(veiculo, concessionaria, cliente, dto.DataVenda, dto.PrecoVenda);
        }

        public static VendaDadosDeCriacaoDto DadosDeCriacao(IEnumerable<VeiculoDto> veiculos, IEnumerable<ConcessionariaDto> concessionarias, IEnumerable<ClienteDto> clientes)
        {
            return new VendaDadosDeCriacaoDto(veiculos, concessionarias, clientes);
        }

        public static VendaDetalhesDto CriarDetalhes(Venda v)
        {
            return new VendaDetalhesDto
            {
                Id = v.Id,
                VeiculoId = v.VeiculoId,
                VeiculoModelo = v.Veiculo?.Modelo ?? "",
                ConcessionariaId = v.ConcessionariaId,
                ConcessionariaNome = v.Concessionaria?.Nome ?? "",
                ClienteId = v.ClienteId,
                ClienteNome = v.Cliente?.Nome ?? "",
                DataVenda = v.DataVenda,
                PrecoVenda = v.PrecoVenda,
                ProtocoloVenda = v.ProtocoloVenda
            };
        }
    }
}
