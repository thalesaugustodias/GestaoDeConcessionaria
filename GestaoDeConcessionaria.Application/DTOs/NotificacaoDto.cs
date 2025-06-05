namespace GestaoDeConcessionaria.Application.DTOs
{
    public record NotificacaoDto(
        string Id,
        int ConcessionariaId,
        bool VendaCriada,
        bool EstoqueEsgotado,
        bool NovoVeiculo,
        bool GaragemLotada,
        IEnumerable<string> Emails,
        IEnumerable<string> Telefones
    );
}
