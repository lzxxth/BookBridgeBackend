public interface ILivroRepository
{
    Task<List<object>> GetLivros();
    Task<object?> GetLivroById(int idLivro);
    Task InsertLivro(string titulo, string? isbn, int idCategoria, int? idDisciplina);
    Task UpdateLivro(int idLivro, string titulo, string? isbn, int idCategoria, int? idDisciplina);
    Task DeleteLivro(int idLivro);
}
