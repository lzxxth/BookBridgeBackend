using Microsoft.Data.SqlClient;

public class AutorRepository : IAutorRepository
{
  private readonly string _connectionString;

  public AutorRepository(IConfiguration config) {
    _connectionString = config.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
  }

  public async Task<List<object>> GetAutores() {
    var results = new List<object>();

    using var connection = new SqlConnection(_connectionString);
    await connection.OpenAsync();

    var query = @"SELECT * FROM BookBridge.dbo.Autor";

    using var command = new SqlCommand(query, connection);
    using var reader = await command.ExecuteReaderAsync();

    while (await reader.ReadAsync()) {
      results.Add(new
        {
          idAutor = reader.GetInt32(0),
          nomeAutor = reader.GetString(1)
        });
      }
      return results;
  }

  public async Task<object?> GetAutorById(int idAutor) {
    using var connection = new SqlConnection(_connectionString);
    await connection.OpenAsync();

    var query = @"SELECT * FROM BookBridge.dbo.Autor WHERE idAutor = @idAutor";

    using var command = new SqlCommand(query, connection);
    command.Parameters.AddWithValue("@idAutor", idAutor);

    using var reader = await command.ExecuteReaderAsync();

    while (await reader.ReadAsync()) {
        return new
        {
          idAutor = reader.GetInt32(0),
          nomeAutor = reader.GetString(1)
        };
      }
      return null;
  }
  
  public async Task InsertAutor(string nomeAutor) {
    using var connection = new SqlConnection(_connectionString);
    await connection.OpenAsync();

    var query = @"INSERT INTO BookBridge.dbo.Autor 
                  (nomeAutor)
                  VALUES (@nomeAutor)";

    using var command = new SqlCommand(query, connection);

    command.Parameters.AddWithValue("@nomeAutor", nomeAutor);

    await command.ExecuteNonQueryAsync();
  }

  public async Task UpdateAutor(int idAutor, string nomeAutor) {
    using var connection = new SqlConnection(_connectionString);
    await connection.OpenAsync();

    var query = @"UPDATE BookBridge.dbo.Autor
                  SET nomeAutor = @nomeAutor
                  WHERE idAutor = @idAutor";

    using var command = new SqlCommand(query, connection);

    command.Parameters.AddWithValue("@idAutor", idAutor);
    command.Parameters.AddWithValue("@nomeAutor", nomeAutor);

    await command.ExecuteNonQueryAsync();
  }

  public async Task DeleteAutor(int idAutor) {
    using var connection = new SqlConnection(_connectionString);
    await connection.OpenAsync();

    var query = @"DELETE FROM BookBridge.dbo.Autor WHERE idAutor = @idAutor";

    using var command = new SqlCommand(query, connection);

    command.Parameters.AddWithValue("@idAutor", idAutor);

    await command.ExecuteNonQueryAsync();
  }
}
