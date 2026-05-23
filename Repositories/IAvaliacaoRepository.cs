public interface IAvaliacaoRepository
{
    Task<List<object>> GetAvaliacoes();
    Task<object?> GetAvaliacaoById(int idAvaliacao);
    Task InsertAvaliacao(int idUserAvaliado, int idUserAvaliador, int idHistorico, int rating, string? comentario);
    Task UpdateAvaliacao(int idAvaliacao, int rating, string? comentario);
    Task DeleteAvaliacao(int idAvaliacao);
}
