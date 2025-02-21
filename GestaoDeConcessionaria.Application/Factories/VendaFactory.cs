using GestaoDeConcessionaria.Domain.Entities;

namespace GestaoDeConcessionaria.Application.Factories
{
    public static class VendaFactory
    {
        public static Venda Criar(Veiculo veiculo, Concessionaria concessionaria, Cliente cliente, DateTime dataVenda, decimal precoVenda)
        {
            return new Venda(veiculo, concessionaria, cliente, dataVenda, precoVenda);
        }
    }
}
