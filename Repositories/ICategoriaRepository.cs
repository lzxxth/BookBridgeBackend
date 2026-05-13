public interface ICategoriaRepository
{
  Task<List<object>> GetCategorias();
  Task<object?> GetCategoriaById(int idCategoria);
  Task InsertCategoria(string nome);
  Task UpdateCategoria(int idCategoria, string nome);
  Task DeleteCategoria(int idCategoria);
}
