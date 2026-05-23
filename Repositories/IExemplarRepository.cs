public interface IExemplarRepository
{
    Task<List<object>> GetExemplares();
    Task<object?> GetExemplarById(int idExemplar);
    Task InsertExemplar(int idLivro, int idProprietario, string estado, bool disponivel, string? descricao);
    Task UpdateExemplar(int idExemplar, int idLivro, int idProprietario, string estado, bool disponivel, string? descricao);
    Task DeleteExemplar(int idExemplar);
}
