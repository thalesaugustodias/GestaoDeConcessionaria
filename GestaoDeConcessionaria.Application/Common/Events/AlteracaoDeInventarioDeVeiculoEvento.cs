namespace GestaoDeConcessionaria.Application.Common.Events
{
    public class AlteracaoDeInventarioDeVeiculoEvento
    {
        public int VeiculoId { get; init; }
        public int ConcessionariaId { get; init; }
        public int NovoEstoque { get; init; }
    }
}
