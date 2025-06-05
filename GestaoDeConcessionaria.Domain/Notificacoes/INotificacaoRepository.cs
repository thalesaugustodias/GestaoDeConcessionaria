namespace GestaoDeConcessionaria.Domain.Notificacoes
{
    public interface INotificacaoRepository
    {
        Task<IEnumerable<NotificacaoConfig>> ObterTodosAsync();
        Task<NotificacaoConfig?> ObterPorConcessionariaIdAsync(int concessionariaId);
        Task CriarAsync(NotificacaoConfig configuracao);
        Task AtualizarAsync(NotificacaoConfig configuracao);
        Task ExcluirAsync(string id);
    }
}
