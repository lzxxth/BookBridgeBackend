public interface IHistoricoRepository
{
    Task<List<object>> GetHistoricos();

    Task<object?> GetHistoricoById(int idHistorico);

    Task InsertHistorico(
        int idTransacao,
        int idUser,
        string? descricao);

    Task UpdateHistorico(
        int idHistorico,
        int idTransacao,
        int idUser,
        string? descricao);

    Task DeleteHistorico(int idHistorico);

    Task<List<object>> GetHistoricoDetalhado();
}