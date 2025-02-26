using GestaoDeConcessionaria.Domain.Entities;
using GestaoDeConcessionaria.Web.Models.Vendas;

namespace GestaoDeConcessionaria.Web.Factories
{
    public static class VendaFactory
    {
        public static class VendaViewModelFactory
        {
            public static VendaViewModel Create(Venda venda)
            {
                if (venda == null)
                    return null;

                return new VendaViewModel
                {
                    Id = venda.Id,
                    VeiculoId = venda.VeiculoId,
                    Modelo = venda.Veiculo?.Modelo,
                    ConcessionariaId = venda.ConcessionariaId,
                    ConcessionariaNome = venda.Concessionaria?.Nome,
                    ClienteId = venda.ClienteId,
                    ClienteNome = venda.Cliente?.Nome,
                    DataVenda = venda.DataVenda,
                    PrecoVenda = venda.PrecoVenda
                };
            }

            public static List<VendaViewModel> Create(IEnumerable<Venda> vendas)
            {
                return vendas.Select(v => Create(v)).ToList();
            }
        }
    }
}
