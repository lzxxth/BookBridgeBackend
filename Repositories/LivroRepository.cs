using Microsoft.Data.SqlClient;

public class LivroRepository : ILivroRepository
{
  private readonly string _connectionString;

  public LivroRepository(IConfiguration config) {
    _connectionString = config.GetConnectionString("DefaultConnection");
  }

  public async Task<List<object>> GetLivros() {
    var results = new List<object>();

    using var connection = new SqlConnection(_connectionString);
    await connection.OpenAsync();

    var query = @"SELECT * FROM BookBridge.dbo.Livro";

    using var command = new SqlCommand(query, connection);
    using var reader = await command.ExecuteReaderAsync();

    while (await reader.ReadAsync()) {
      results.Add(new
        {
          idLivro = reader.GetInt32(0),
          titulo = reader.GetString(1),
          idCategoria = reader.GetInt32(2),
          idAutor = reader.GetInt32(3)
        });
      }
      return results;
  }

  public async Task<object?> GetLivroById(int idLivro) {
    using var connection = new SqlConnection(_connectionString);
    await connection.OpenAsync();
    
    var query = @"SELECT * FROM BookBridge.dbo.Livro WHERE idLivro = @idLivro";
    
    using var command = new SqlCommand(query, connection);
    command.Parameters.AddWithValue("@idLivro", idLivro);
    
    using var reader = await command.ExecuteReaderAsync();

    while (await reader.ReadAsync()) {
        return new
        {
          idLivro = reader.GetInt32(0),
          titulo = reader.GetString(1),
          idCategoria = reader.GetInt32(2),
          idAutor = reader.GetInt32(3)
        };
      }
      return null;
  }

  public async Task InsertLivro(string titulo, int idCategoria, int idAutor) {
    using var connection = new SqlConnection(_connectionString);
    await connection.OpenAsync();

    var query = @"INSERT INTO BookBridge.dbo.Livro 
                  (titulo, idCategoria, idAutor)
                  VALUES (@titulo, @idCategoria, @idAutor)";

    using var command = new SqlCommand(query, connection);

    command.Parameters.AddWithValue("@titulo", titulo);
    command.Parameters.AddWithValue("@idCategoria", idCategoria);
    command.Parameters.AddWithValue("@idAutor", idAutor);

    await command.ExecuteNonQueryAsync();
  }

  public async Task UpdateLivro(int idLivro, string titulo, int idCategoria, int idAutor) {
    using var connection = new SqlConnection(_connectionString);
    await connection.OpenAsync();

    var query = @"UPDATE BookBridge.dbo.Livro
                SET titulo = @titulo,
                    idCategoria = @idCategoria,
                    idAutor = @idAutor
                WHERE idLivro = @idLivro";

    using var command = new SqlCommand(query, connection);

    command.Parameters.AddWithValue("@idLivro", idLivro);
    command.Parameters.AddWithValue("@titulo", titulo);
    command.Parameters.AddWithValue("@idCategoria", idCategoria);
    command.Parameters.AddWithValue("@idAutor", idAutor);

    await command.ExecuteNonQueryAsync();
  }

  public async Task DeleteLivro(int idLivro) {
    using var connection = new SqlConnection(_connectionString);
    await connection.OpenAsync();

    var query = @"DELETE FROM BookBridge.dbo.Livro WHERE idLivro = @idLivro";

    using var command = new SqlCommand(query, connection);

    command.Parameters.AddWithValue("@idLivro", idLivro);

    await command.ExecuteNonQueryAsync();
  }
}
