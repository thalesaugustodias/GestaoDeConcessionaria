using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GestaoDeConcessionaria.Domain.Notificacoes
{
    public class NotificacaoConfig
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public int ConcessionariaId { get; set; }

        public bool VendaCriada { get; set; }
        public bool EstoqueEsgotado { get; set; }
        public bool NovoVeiculo { get; set; }
        public bool GaragemLotada { get; set; }

        public List<string> Emails { get; set; } = new();
        public List<string> Telefones { get; set; } = new();
    }
}
