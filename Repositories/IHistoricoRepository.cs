public interface IHistoricoRepository
{
    Task<List<object>> GetHistoricos();
    Task<object?> GetHistoricoById(int idHistorico);
    Task InsertHistorico(int idTipoTransacao, int idUserSolicitante, int idUserProprietario, int idExemplar, string estado, string? observacoes);
    Task UpdateHistorico(int idHistorico, int idTipoTransacao, int idUserSolicitante, int idUserProprietario, int idExemplar, string estado, DateOnly? dataConclusao, string? observacoes);
    Task DeleteHistorico(int idHistorico);
}
