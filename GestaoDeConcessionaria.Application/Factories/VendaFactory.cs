using GestaoDeConcessionaria.Application.DTOs;
using GestaoDeConcessionaria.Domain.Entities;

namespace GestaoDeConcessionaria.Application.Factories
{
    public static class VendaFactory
    {
        public static Venda Criar(VendaDTO dto, Veiculo veiculo, Concessionaria concessionaria, Cliente cliente)
        {
            return new Venda(veiculo, concessionaria, cliente, dto.DataVenda, dto.PrecoVenda);
        }
    }
}
