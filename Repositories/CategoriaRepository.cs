using Microsoft.Data.SqlClient;

public class CategoriaRepository : ICategoriaRepository
{
  private readonly string _connectionString;

  public CategoriaRepository(IConfiguration config) {
    _connectionString = config.GetConnectionString("DefaultConnection");
  }

  public async Task<List<object>> GetCategorias() {
    var results = new List<object>();

    using var connection = new SqlConnection(_connectionString);
    await connection.OpenAsync();

    var query = @"SELECT * FROM BookBridge.dbo.Categoria";

    using var command = new SqlCommand(query, connection);
    using var reader = await command.ExecuteReaderAsync();

    while (await reader.ReadAsync()) {
      results.Add(new
        {
          idCategoria = reader.GetInt32(0),
          nome = reader.GetString(1)
        });
      }
      return results;
  }

  public async Task<object?> GetCategoriaById(int idCategoria) {
    using var connection = new SqlConnection(_connectionString);
    await connection.OpenAsync();

    var query = @"SELECT * FROM BookBridge.dbo.Categoria WHERE idCategoria = @idCategoria";

    using var command = new SqlCommand(query, connection);
    command.Parameters.AddWithValue("@idCategoria", idCategoria);

    using var reader = await command.ExecuteReaderAsync();

    while (await reader.ReadAsync()) {
      return new
        {
          idCategoria = reader.GetInt32(0),
          nome = reader.GetString(1)
        };
      }
      return null;
  }
  public async Task InsertCategoria(string nome) {
    using var connection = new SqlConnection(_connectionString);
    await connection.OpenAsync();

    var query = @"INSERT INTO BookBridge.dbo.Categoria 
                  (nome)
                  VALUES (@nome)";

    using var command = new SqlCommand(query, connection);

    command.Parameters.AddWithValue("@nome", nome);

    await command.ExecuteNonQueryAsync();
  }
  
  public async Task UpdateCategoria(int idCategoria, string nome) {
    using var connection = new SqlConnection(_connectionString);
    await connection.OpenAsync();

    var query = @"UPDATE FROM BookBridge.dbo.Categoria 
                  SET nome = @nome
                  WHERE idCategoria = @idCategoria";

    using var command = new SqlCommand(query, connection);

    command.Parameters.AddWithValue("@nome", nome);
    command.Parameters.AddWithValue("@idCategoria", idCategoria);

    await command.ExecuteNonQueryAsync();
  }
  
  public async Task DeleteCategoria(int idCategoria) {
    using var connection = new SqlConnection(_connectionString);
    await connection.OpenAsync();

    var query = @"DELETE FROM BookBridge.dbo.Autor WHERE idCategoria = @idCategoria";

    using var command = new SqlCommand(query, connection);

    command.Parameters.AddWithValue("@idCategoria", idCategoria);

    await command.ExecuteNonQueryAsync();
  }
}
