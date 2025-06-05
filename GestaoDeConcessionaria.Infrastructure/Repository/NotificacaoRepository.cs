using GestaoDeConcessionaria.Domain.Notificacoes;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace GestaoDeConcessionaria.Infrastructure.Repository
{

    public class NotificacaoRepository : INotificacaoRepository
    {
        private readonly IMongoCollection<NotificacaoConfig> _collection;

        public NotificacaoRepository(IConfiguration config)
        {
            var client = new MongoClient(config["Mongo:ConnectionString"]);
            var db = client.GetDatabase(config["Mongo:Database"]);
            _collection = db.GetCollection<NotificacaoConfig>("notificacao_config");
        }

        public async Task<IEnumerable<NotificacaoConfig>> ObterTodosAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<NotificacaoConfig?> ObterPorConcessionariaIdAsync(int concessionariaId)
        {
            return await _collection
                .Find(ns => ns.ConcessionariaId == concessionariaId)
                .FirstOrDefaultAsync();
        }

        public async Task CriarAsync(NotificacaoConfig setting)
        {
            await _collection.InsertOneAsync(setting);
        }

        public async Task AtualizarAsync(NotificacaoConfig setting)
        {
            await _collection.ReplaceOneAsync(
                ns => ns.Id == setting.Id,
                setting);
        }

        public async Task ExcluirAsync(string id)
        {
            await _collection.DeleteOneAsync(ns => ns.Id == id);
        }
    }

}
