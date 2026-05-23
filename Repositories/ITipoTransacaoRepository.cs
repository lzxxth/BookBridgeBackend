public interface ITipoTransacaoRepository
{
    Task<List<object>> GetTipoTransacoes();
    Task<object?> GetTipoTransacaoById(int idTipoTransacao);
    Task InsertTipoTransacao(string descricao);
    Task UpdateTipoTransacao(int idTipoTransacao, string descricao);
    Task DeleteTipoTransacao(int idTipoTransacao);
}
