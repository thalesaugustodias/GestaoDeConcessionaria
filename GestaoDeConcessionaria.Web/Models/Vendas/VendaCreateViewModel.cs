namespace GestaoDeConcessionaria.Web.Models.Vendas
{
    public record VendaCreateViewModel(IEnumerable<VeiculoViewModel> Veiculos, IEnumerable<ConcessionariaViewModel> Concessionarias, IEnumerable<ClienteViewModel> Clientes);
}
