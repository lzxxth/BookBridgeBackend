using Microsoft.Data.SqlClient;
public class TipoAnuncioRepository : ITipoAnuncioRepository
{
  private readonly string _connectionString;
  public TipoAnuncioRepository(IConfiguration config) {
    _connectionString = config.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
  }
  public async Task<List<object>> GetTipoAnuncios() {
    var results = new List<object>();
    using var connection = new SqlConnection(_connectionString);
    await connection.OpenAsync();
    var query = @"SELECT * FROM BookBridge.dbo.TipoAnuncio";
    using var command = new SqlCommand(query, connection);
    using var reader = await command.ExecuteReaderAsync();
    while (await reader.ReadAsync()) {
      results.Add(new
        {
          idTipoAnuncio = reader.GetInt32(0),
          descricao     = reader.GetString(1)
        });
      }
      return results;
  }
  public async Task<object?> GetTipoAnuncioById(int idTipoAnuncio) {
    using var connection = new SqlConnection(_connectionString);
    await connection.OpenAsync();
    var query = @"SELECT * FROM BookBridge.dbo.TipoAnuncio WHERE idTipoAnuncio = @idTipoAnuncio";
    using var command = new SqlCommand(query, connection);
    command.Parameters.AddWithValue("@idTipoAnuncio", idTipoAnuncio);
    using var reader = await command.ExecuteReaderAsync();
    while (await reader.ReadAsync()) {
      return new
        {
          idTipoAnuncio = reader.GetInt32(0),
          descricao     = reader.GetString(1)
        };
      }
      return null;
  }
  public async Task InsertTipoAnuncio(string descricao) {
    using var connection = new SqlConnection(_connectionString);
    await connection.OpenAsync();
    var query = @"INSERT INTO BookBridge.dbo.TipoAnuncio 
                  (descricao)
                  VALUES (@descricao)";
    using var command = new SqlCommand(query, connection);
    command.Parameters.AddWithValue("@descricao", descricao);
    await command.ExecuteNonQueryAsync();
  }
  public async Task UpdateTipoAnuncio(int idTipoAnuncio, string descricao) {
    using var connection = new SqlConnection(_connectionString);
    await connection.OpenAsync();
    var query = @"UPDATE BookBridge.dbo.TipoAnuncio
                  SET descricao = @descricao
                  WHERE idTipoAnuncio = @idTipoAnuncio";
    using var command = new SqlCommand(query, connection);
    command.Parameters.AddWithValue("@idTipoAnuncio", idTipoAnuncio);
    command.Parameters.AddWithValue("@descricao",     descricao);
    await command.ExecuteNonQueryAsync();
  }
  public async Task DeleteTipoAnuncio(int idTipoAnuncio) {
    using var connection = new SqlConnection(_connectionString);
    await connection.OpenAsync();
    var query = @"DELETE FROM BookBridge.dbo.TipoAnuncio WHERE idTipoAnuncio = @idTipoAnuncio";
    using var command = new SqlCommand(query, connection);
    command.Parameters.AddWithValue("@idTipoAnuncio", idTipoAnuncio);
    await command.ExecuteNonQueryAsync();
  }
}
