public interface ILivroRepository
{
  Task<List<object>> GetLivros();
  Task<object?> GetLivroById(int idLivro);
  Task InsertLivro(string titulo, int idCategoria, int idAutor);
  Task UpdateLivro(int idLivro, string titulo, int idCategoria, int idAutor);
  Task DeleteLivro(int idLivro);
}
