using System.Data;
using Microsoft.Data.SqlClient;
public class AnuncioRepository : IAnuncioRepository
{
  private readonly string _connectionString;
  public AnuncioRepository(IConfiguration config) {
    _connectionString = config.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
  }
  public async Task<List<object>> GetAnuncios() {
    var results = new List<object>();
    using var connection = new SqlConnection(_connectionString);
    await connection.OpenAsync();
    var query = @"SELECT * FROM BookBridge.dbo.Anuncio";
    using var command = new SqlCommand(query, connection);
    using var reader = await command.ExecuteReaderAsync();
    while (await reader.ReadAsync()) {
      results.Add(new
        {
          idAnuncio     = reader.GetInt32(0),
          idExemplar    = reader.GetInt32(1),
          idUser        = reader.GetInt32(2),
          idTipoAnuncio = reader.GetInt32(3),
          titulo        = reader.GetString(4),
          descricao     = reader.IsDBNull(5) ? null : reader.GetString(5),
          preco         = reader.IsDBNull(6) ? (decimal?)null : reader.GetDecimal(6),
          estado        = reader.GetString(7),
          dataPublicacao = reader.GetDateTime(8)
        });
      }
      return results;
  }
  public async Task<object?> GetAnuncioById(int idAnuncio) {
    using var connection = new SqlConnection(_connectionString);
    await connection.OpenAsync();
    var query = @"SELECT * FROM BookBridge.dbo.Anuncio WHERE idAnuncio = @idAnuncio";
    using var command = new SqlCommand(query, connection);
    command.Parameters.AddWithValue("@idAnuncio", idAnuncio);
    using var reader = await command.ExecuteReaderAsync();
    while (await reader.ReadAsync()) {
      return new
        {
          idAnuncio     = reader.GetInt32(0),
          idExemplar    = reader.GetInt32(1),
          idUser        = reader.GetInt32(2),
          idTipoAnuncio = reader.GetInt32(3),
          titulo        = reader.GetString(4),
          descricao     = reader.IsDBNull(5) ? null : reader.GetString(5),
          preco         = reader.IsDBNull(6) ? (decimal?)null : reader.GetDecimal(6),
          estado        = reader.GetString(7),
          dataPublicacao = reader.GetDateTime(8)
        };
      }
      return null;
  }
  public async Task<List<object>> GetAnunciosByUser(int idUser) {
    var results = new List<object>();
    using var connection = new SqlConnection(_connectionString);
    await connection.OpenAsync();
    var query = @"SELECT * FROM BookBridge.dbo.Anuncio WHERE idUser = @idUser";
    using var command = new SqlCommand(query, connection);
    command.Parameters.AddWithValue("@idUser", idUser);
    using var reader = await command.ExecuteReaderAsync();
    while (await reader.ReadAsync()) {
      results.Add(new
        {
          idAnuncio     = reader.GetInt32(0),
          idExemplar    = reader.GetInt32(1),
          idUser        = reader.GetInt32(2),
          idTipoAnuncio = reader.GetInt32(3),
          titulo        = reader.GetString(4),
          descricao     = reader.IsDBNull(5) ? null : reader.GetString(5),
          preco         = reader.IsDBNull(6) ? (decimal?)null : reader.GetDecimal(6),
          estado        = reader.GetString(7),
          dataPublicacao = reader.GetDateTime(8)
        });
      }
      return results;
  }
  public async Task<List<object>> GetAnunciosByTipo(int idTipoAnuncio) {
    var results = new List<object>();
    using var connection = new SqlConnection(_connectionString);
    await connection.OpenAsync();
    var query = @"SELECT * FROM BookBridge.dbo.Anuncio WHERE idTipoAnuncio = @idTipoAnuncio";
    using var command = new SqlCommand(query, connection);
    command.Parameters.AddWithValue("@idTipoAnuncio", idTipoAnuncio);
    using var reader = await command.ExecuteReaderAsync();
    while (await reader.ReadAsync()) {
      results.Add(new
        {
          idAnuncio     = reader.GetInt32(0),
          idExemplar    = reader.GetInt32(1),
          idUser        = reader.GetInt32(2),
          idTipoAnuncio = reader.GetInt32(3),
          titulo        = reader.GetString(4),
          descricao     = reader.IsDBNull(5) ? null : reader.GetString(5),
          preco         = reader.IsDBNull(6) ? (decimal?)null : reader.GetDecimal(6),
          estado        = reader.GetString(7),
          dataPublicacao = reader.GetDateTime(8)
        });
      }
      return results;
  }
  public async Task InsertAnuncio(int idExemplar, int idUser, int idTipoAnuncio, string titulo, string? descricao, decimal? preco) {
    using var connection = new SqlConnection(_connectionString);
    await connection.OpenAsync();
    var query = @"INSERT INTO BookBridge.dbo.Anuncio 
                  (idExemplar, idUser, idTipoAnuncio, titulo, descricao, preco)
                  VALUES (@idExemplar, @idUser, @idTipoAnuncio, @titulo, @descricao, @preco)";
    using var command = new SqlCommand(query, connection);
    command.Parameters.AddWithValue("@idExemplar",    idExemplar);
    command.Parameters.AddWithValue("@idUser",        idUser);
    command.Parameters.AddWithValue("@idTipoAnuncio", idTipoAnuncio);
    command.Parameters.AddWithValue("@titulo",        titulo);
    command.Parameters.AddWithValue("@descricao",     (object?)descricao ?? DBNull.Value);
    command.Parameters.AddWithValue("@preco",         (object?)preco     ?? DBNull.Value);
    await command.ExecuteNonQueryAsync();
  }
  public async Task UpdateAnuncio(int idAnuncio, int idTipoAnuncio, string titulo, string? descricao, decimal? preco, string estado) {
    using var connection = new SqlConnection(_connectionString);
    await connection.OpenAsync();
    var query = @"UPDATE BookBridge.dbo.Anuncio
                  SET idTipoAnuncio = @idTipoAnuncio,
                      titulo        = @titulo,
                      descricao     = @descricao,
                      preco         = @preco,
                      estado        = @estado
                  WHERE idAnuncio = @idAnuncio";
    using var command = new SqlCommand(query, connection);
    command.Parameters.AddWithValue("@idAnuncio",     idAnuncio);
    command.Parameters.AddWithValue("@idTipoAnuncio", idTipoAnuncio);
    command.Parameters.AddWithValue("@titulo",        titulo);
    command.Parameters.AddWithValue("@descricao",     (object?)descricao ?? DBNull.Value);
    command.Parameters.AddWithValue("@preco",         (object?)preco     ?? DBNull.Value);
    command.Parameters.AddWithValue("@estado",        estado);
    await command.ExecuteNonQueryAsync();
  }
  public async Task DeleteAnuncio(int idAnuncio) {
    using var connection = new SqlConnection(_connectionString);
    await connection.OpenAsync();
    var query = @"DELETE FROM BookBridge.dbo.Anuncio WHERE idAnuncio = @idAnuncio";
    using var command = new SqlCommand(query, connection);
    command.Parameters.AddWithValue("@idAnuncio", idAnuncio);
    await command.ExecuteNonQueryAsync();
  }

  public async Task<bool> AnuncioDisponivel(int idAnuncio)
  {
      using var connection = new SqlConnection(_connectionString);
      await connection.OpenAsync();

      var query = "SELECT dbo.fn_AnuncioDisponivel(@idAnuncio)";

      using var command = new SqlCommand(query, connection);
      command.Parameters.AddWithValue("@idAnuncio", idAnuncio);

      var result = await command.ExecuteScalarAsync();

      return Convert.ToBoolean(result);
  }

  public async Task<List<object>> GetAnunciosAtivos()
  {
      var results = new List<object>();

      using var connection = new SqlConnection(_connectionString);
      await connection.OpenAsync();

      var query = "SELECT * FROM dbo.vw_AnunciosAtivos";

      using var command = new SqlCommand(query, connection);
      using var reader = await command.ExecuteReaderAsync();

      while (await reader.ReadAsync())
      {
          results.Add(new
          {
              idAnuncio = reader.GetInt32(0),
              titulo = reader.GetString(1),
              descricao = reader.IsDBNull(2) ? null : reader.GetString(2),
              preco = reader.IsDBNull(3)
                ? (decimal?)null
                : reader.GetDecimal(3),
              tipoAnuncio = reader.GetString(4),
              tituloLivro = reader.GetString(5),
              isbn = reader.IsDBNull(6) ? null : reader.GetString(6),
              autores = reader.IsDBNull(7) ? null : reader.GetString(7),
              estadoExemplar = reader.GetString(8),
              nomeProprietario = reader.GetString(9),
              dataPublicacao = reader.GetDateTime(10)
          });
      }

      return results;
  }

  public async Task<int> PublicarAnuncio(
    int idExemplar,
    int idUser,
    int idTipoAnuncio,
    string titulo,
    string? descricao,
    decimal? preco)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        using var command = new SqlCommand("dbo.sp_PublicarAnuncio", connection);
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@idExemplar", idExemplar);
        command.Parameters.AddWithValue("@idUser", idUser);
        command.Parameters.AddWithValue("@idTipoAnuncio", idTipoAnuncio);
        command.Parameters.AddWithValue("@titulo", titulo);
        command.Parameters.AddWithValue("@descricao", (object?)descricao ?? DBNull.Value);
        command.Parameters.AddWithValue("@preco", (object?)preco ?? DBNull.Value);

        var output = new SqlParameter("@idAnuncio", SqlDbType.Int)
        {
            Direction = ParameterDirection.Output
        };

        command.Parameters.Add(output);

        await command.ExecuteNonQueryAsync();

        return (int)output.Value;
    }

    public async Task<List<object>> PesquisarAnuncios(
    string? titulo,
    string? nomeAutor,
    int? idCategoria,
    int? idDisciplina,
    int? idTipoAnuncio)
    {
        var results = new List<object>();

        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        using var command = new SqlCommand("dbo.sp_PesquisarAnuncios", connection);
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@titulo", (object?)titulo ?? DBNull.Value);
        command.Parameters.AddWithValue("@nomeAutor", (object?)nomeAutor ?? DBNull.Value);
        command.Parameters.AddWithValue("@idCategoria", (object?)idCategoria ?? DBNull.Value);
        command.Parameters.AddWithValue("@idDisciplina", (object?)idDisciplina ?? DBNull.Value);
        command.Parameters.AddWithValue("@idTipoAnuncio", (object?)idTipoAnuncio ?? DBNull.Value);

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            results.Add(new
            {
                idAnuncio = reader.GetInt32(0),
                titulo = reader.GetString(1),
                preco = reader.GetDecimal(2),
                tipoAnuncio = reader.GetString(3),
                tituloLivro = reader.GetString(4),
                autores = reader.GetString(5),
                estadoExemplar = reader.GetString(6),
                proprietario = reader.GetString(7),
                dataPublicacao = reader.GetDateTime(8)
            });
        }

        return results;
    }
}
