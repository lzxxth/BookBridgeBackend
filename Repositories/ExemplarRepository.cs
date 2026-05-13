using Microsoft.Data.SqlClient;

public class ExemplarRepository : IExemplarRepository
{
  private readonly string _connectionString;

  public ExemplarRepository(IConfiguration config) {
    _connectionString = config.GetConnectionString("DefaultConnection");
  }
  
  public async Task<List<object>> GetExemplares() {
    var results = new List<object>();

    using var connection = new SqlConnection(_connectionString);
    await connection.OpenAsync();

    var query = @"SELECT * FROM BookBridge.dbo.Exemplar";

    using var command = new SqlCommand(query, connection);
    using var reader = await command.ExecuteReaderAsync();

    while (await reader.ReadAsync()) {
      results.Add(new
        {
          idExemplar = reader.GetInt32(0),
          idLivro = reader.GetInt32(1),
          idProprietario = reader.GetInt32(2),
          estado = reader.GetString(3),
          disponivel = reader.GetBoolean(4),
          dataAdicionado = reader.GetDateTime(5),
        });
      }
      return results;
  }
  
  public async Task<object?> GetExemplarById(int idExemplar) {
    using var connection = new SqlConnection(_connectionString);
    await connection.OpenAsync();
    
    var query = @"SELECT * FROM BookBridge.dbo.Exemplar WHERE idExemplar = @idExemplar";
    
    using var command = new SqlCommand(query, connection);
    command.Parameters.AddWithValue("@idExemplar", idExemplar);
    
    using var reader = await command.ExecuteReaderAsync();

    while (await reader.ReadAsync()) {
        return new
        {
          idExemplar = reader.GetInt32(0),
          idLivro = reader.GetInt32(1),
          idProprietario = reader.GetInt32(2),
          estado = reader.GetString(3),
          disponivel = reader.GetBoolean(4),
          dataAdicionado = reader.GetDateTime(5),
        };
      }
      return null;
  }

  public async Task InsertExemplar(int idLivro, int idProprietario, string estado, bool disponivel, DateTime dataAdicionado) {
    using var connection = new SqlConnection(_connectionString);
    await connection.OpenAsync();

    var query = @"INSERT INTO BookBridge.dbo.Exemplar 
                  (idLivro, idProprietario,  estado, disponivel, dataAdicionado)
                  VALUES (@idLivro, @idProprietario, @estado, @disponivel, @dataAdicionado)";

    using var command = new SqlCommand(query, connection);

    command.Parameters.AddWithValue("@idLivro", idLivro);
    command.Parameters.AddWithValue("@idProprietario", idProprietario);
    command.Parameters.AddWithValue("@estado", estado);
    command.Parameters.AddWithValue("@disponivel", disponivel);
    command.Parameters.AddWithValue("@dataAdicionado", dataAdicionado);

    await command.ExecuteNonQueryAsync();
  }

  public async Task UpdateExemplar(int idExemplar, int idLivro, int idProprietario, string estado, bool disponivel, DateTime dataAdicionado) {
    using var connection = new SqlConnection(_connectionString);
    await connection.OpenAsync();

    var query = @"UPDATE BookBridge.dbo.Exemplar
                  SET idLivro = @idLivro,
                    idProprietario = @idProprietario,
                    estado = @estado,
                    disponivel = @disponivel,
                    dataAdicionado = @dataAdicionado
                  WHERE idExemplar = @idExemplar";

    using var command = new SqlCommand(query, connection);

    command.Parameters.AddWithValue("@idExemplar", idExemplar);
    command.Parameters.AddWithValue("@idLivro", idLivro);
    command.Parameters.AddWithValue("@idProprietario", idProprietario);
    command.Parameters.AddWithValue("@estado", estado);
    command.Parameters.AddWithValue("@disponivel", disponivel);
    command.Parameters.AddWithValue("@dataAdicionado", dataAdicionado);

    await command.ExecuteNonQueryAsync();
  }

  public async Task DeleteExemplar(int idExemplar) {
    using var connection = new SqlConnection(_connectionString);
    await connection.OpenAsync();

    var query = @"DELETE FROM BookBridge.dbo.Exemplar WHERE idExemplar = @idExemplar";

    using var command = new SqlCommand(query, connection);

    command.Parameters.AddWithValue("@idExemplar", idExemplar);

    await command.ExecuteNonQueryAsync();
  }
}
