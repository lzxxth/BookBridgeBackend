public interface IAutorRepository
{
  Task<List<object>> GetAutores();
  Task<object?> GetAutorById(int idAutor);
  Task InsertAutor(string nomeAutor);
  Task UpdateAutor(int idAutor, string nomeAutor);
  Task DeleteAutor(int idAutor);
}
