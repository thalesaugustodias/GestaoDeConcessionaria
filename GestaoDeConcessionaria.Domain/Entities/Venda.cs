namespace GestaoDeConcessionaria.Domain.Entities
{
    public class Venda
    {
        public int Id { get; private set; }
        public int VeiculoId { get; private set; }
        public Veiculo Veiculo { get; private set; }
        public int ConcessionariaId { get; private set; }
        public Concessionaria Concessionaria { get; private set; }
        public int ClienteId { get; private set; }
        public Cliente Cliente { get; private set; }
        public DateTime DataVenda { get; private set; }
        public decimal PrecoVenda { get; private set; }
        public string ProtocoloVenda { get; private set; }
        public bool Ativo { get; private set; }

        private Venda() { }

        public Venda(Veiculo veiculo, Concessionaria concessionaria, Cliente cliente, DateTime dataVenda, decimal precoVenda)
        {
            Validar(veiculo, concessionaria, cliente, dataVenda, precoVenda);
            Veiculo = veiculo;
            VeiculoId = veiculo.Id;
            Concessionaria = concessionaria;
            ConcessionariaId = concessionaria.Id;
            Cliente = cliente;
            ClienteId = cliente.Id;
            DataVenda = dataVenda;
            PrecoVenda = precoVenda;
            ProtocoloVenda = GerarProtocolo();
            Ativo = true;
        }

        private static void Validar(Veiculo veiculo, Concessionaria concessionaria, Cliente cliente, DateTime dataVenda, decimal precoVenda)
        {
            if (veiculo == null)
                throw new ArgumentException("Veículo inválido.");
            if (concessionaria == null)
                throw new ArgumentException("Concessionária inválida.");
            if (cliente == null)
                throw new ArgumentException("Cliente inválido.");
            if (dataVenda > DateTime.Now)
                throw new ArgumentException("Data da venda não pode ser futura.");
            if (precoVenda <= 0 || precoVenda > veiculo.Preco)
                throw new ArgumentException("Preço de venda inválido.");
        }

        private string GerarProtocolo()
        {
            return $"VEN-{DateTime.Now:yyyyMMddHHmmss}-{new Random().Next(1000, 9999)}";
        }

        public void Deletar() => Ativo = false;
    }
}
