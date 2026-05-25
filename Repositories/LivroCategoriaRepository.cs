using Microsoft.Data.SqlClient;
public class LivroCategoriaRepository : ILivroCategoriaRepository
{
  private readonly string _connectionString;
  public LivroCategoriaRepository(IConfiguration config) {
    _connectionString = config.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
  }
  public async Task<List<object>> GetLivroCategorias() {
    var results = new List<object>();
    using var connection = new SqlConnection(_connectionString);
    await connection.OpenAsync();
    var query = @"SELECT * FROM BookBridge.dbo.LivroCategoria";
    using var command = new SqlCommand(query, connection);
    using var reader = await command.ExecuteReaderAsync();
    while (await reader.ReadAsync()) {
      results.Add(new
        {
          idLivro     = reader.GetInt32(0),
          idCategoria = reader.GetInt32(1)
        });
      }
      return results;
  }
  public async Task<List<object>> GetCategoriasByLivro(int idLivro) {
    var results = new List<object>();
    using var connection = new SqlConnection(_connectionString);
    await connection.OpenAsync();
    var query = @"SELECT * FROM BookBridge.dbo.LivroCategoria WHERE idLivro = @idLivro";
    using var command = new SqlCommand(query, connection);
    command.Parameters.AddWithValue("@idLivro", idLivro);
    using var reader = await command.ExecuteReaderAsync();
    while (await reader.ReadAsync()) {
      results.Add(new
        {
          idLivro     = reader.GetInt32(0),
          idCategoria = reader.GetInt32(1)
        });
      }
      return results;
  }
  public async Task<List<object>> GetLivrosByCategoria(int idCategoria) {
    var results = new List<object>();
    using var connection = new SqlConnection(_connectionString);
    await connection.OpenAsync();
    var query = @"SELECT * FROM BookBridge.dbo.LivroCategoria WHERE idCategoria = @idCategoria";
    using var command = new SqlCommand(query, connection);
    command.Parameters.AddWithValue("@idCategoria", idCategoria);
    using var reader = await command.ExecuteReaderAsync();
    while (await reader.ReadAsync()) {
      results.Add(new
        {
          idLivro     = reader.GetInt32(0),
          idCategoria = reader.GetInt32(1)
        });
      }
      return results;
  }
  public async Task InsertLivroCategoria(int idLivro, int idCategoria) {
    using var connection = new SqlConnection(_connectionString);
    await connection.OpenAsync();
    var query = @"INSERT INTO BookBridge.dbo.LivroCategoria 
                  (idLivro, idCategoria)
                  VALUES (@idLivro, @idCategoria)";
    using var command = new SqlCommand(query, connection);
    command.Parameters.AddWithValue("@idLivro",     idLivro);
    command.Parameters.AddWithValue("@idCategoria", idCategoria);
    await command.ExecuteNonQueryAsync();
  }
  public async Task DeleteLivroCategoria(int idLivro, int idCategoria) {
    using var connection = new SqlConnection(_connectionString);
    await connection.OpenAsync();
    var query = @"DELETE FROM BookBridge.dbo.LivroCategoria 
                  WHERE idLivro = @idLivro AND idCategoria = @idCategoria";
    using var command = new SqlCommand(query, connection);
    command.Parameters.AddWithValue("@idLivro",     idLivro);
    command.Parameters.AddWithValue("@idCategoria", idCategoria);
    await command.ExecuteNonQueryAsync();
  }
}