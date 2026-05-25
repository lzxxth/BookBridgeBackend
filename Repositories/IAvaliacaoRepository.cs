public interface IAvaliacaoRepository
{
    Task<List<object>> GetAvaliacoes();

    Task<object?> GetAvaliacaoById(int idAvaliacao);

    Task InsertAvaliacao(
        int idUserAvaliado,
        int idUserAvaliador,
        int idTransacao,
        int rating,
        string? comentario);

    Task UpdateAvaliacao(
        int idAvaliacao,
        int idUserAvaliado,
        int idUserAvaliador,
        int idTransacao,
        int rating,
        string? comentario);

    Task DeleteAvaliacao(int idAvaliacao);
    Task<decimal> GetMediaAvaliacao(int idUser);
    Task<List<object>> GetAvaliacoesDetalhadas();
}