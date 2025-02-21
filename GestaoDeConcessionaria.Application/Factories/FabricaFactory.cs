using GestaoDeConcessionaria.Domain.Entities;

namespace GestaoDeConcessionaria.Application.Factories
{
    public static class FabricaFactory
    {
        public static Fabricante Criar(string nome, string paisOrigem, int anoFundacao, string website)
        {
            return new Fabricante(nome, paisOrigem, anoFundacao, website);
        }
    }
}
