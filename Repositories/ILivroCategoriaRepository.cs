public interface ILivroCategoriaRepository
{
  Task<List<object>> GetLivroCategorias();
  Task<List<object>> GetCategoriasByLivro(int idLivro);
  Task<List<object>> GetLivrosByCategoria(int idCategoria);
  Task InsertLivroCategoria(int idLivro, int idCategoria);
  Task DeleteLivroCategoria(int idLivro, int idCategoria);
}