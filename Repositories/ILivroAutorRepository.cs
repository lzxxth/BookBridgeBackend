public interface ILivroAutorRepository
{
    Task<List<object>> GetLivroAutores();
    Task<List<object>> GetAutoresByLivro(int idLivro);
    Task<List<object>> GetLivrosByAutor(int idAutor);
    Task InsertLivroAutor(int idLivro, int idAutor, int ordemAutor);
    Task UpdateLivroAutor(int idLivro, int idAutor, int ordemAutor);
    Task DeleteLivroAutor(int idLivro, int idAutor);
}
