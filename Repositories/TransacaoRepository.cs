using System.Data;
using Microsoft.Data.SqlClient;
public class TransacaoRepository : ITransacaoRepository
{
  private readonly string _connectionString;
  public TransacaoRepository(IConfiguration config) {
    _connectionString = config.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
  }
  public async Task<List<object>> GetTransacoes() {
    var results = new List<object>();
    using var connection = new SqlConnection(_connectionString);
    await connection.OpenAsync();
    var query = @"SELECT * FROM BookBridge.dbo.Transacao";
    using var command = new SqlCommand(query, connection);
    using var reader = await command.ExecuteReaderAsync();
    while (await reader.ReadAsync()) {
      results.Add(new
        {
          idTransacao     = reader.GetInt32(0),
          idAnuncio       = reader.GetInt32(1),
          idUserComprador = reader.GetInt32(2),
          estado          = reader.GetString(3),
          dataInicio      = reader.GetDateTime(4),
          dataFimPrevista = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5),
          dataFimEfetiva  = reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6),
          observacoes     = reader.IsDBNull(7) ? null : reader.GetString(7)
        });
      }
      return results;
  }
  public async Task<object?> GetTransacaoById(int idTransacao) {
    using var connection = new SqlConnection(_connectionString);
    await connection.OpenAsync();
    var query = @"SELECT * FROM BookBridge.dbo.Transacao WHERE idTransacao = @idTransacao";
    using var command = new SqlCommand(query, connection);
    command.Parameters.AddWithValue("@idTransacao", idTransacao);
    using var reader = await command.ExecuteReaderAsync();
    while (await reader.ReadAsync()) {
      return new
        {
          idTransacao     = reader.GetInt32(0),
          idAnuncio       = reader.GetInt32(1),
          idUserComprador = reader.GetInt32(2),
          estado          = reader.GetString(3),
          dataInicio      = reader.GetDateTime(4),
          dataFimPrevista = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5),
          dataFimEfetiva  = reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6),
          observacoes     = reader.IsDBNull(7) ? null : reader.GetString(7)
        };
      }
      return null;
  }
  public async Task<List<object>> GetTransacoesByAnuncio(int idAnuncio) {
    var results = new List<object>();
    using var connection = new SqlConnection(_connectionString);
    await connection.OpenAsync();
    var query = @"SELECT * FROM BookBridge.dbo.Transacao WHERE idAnuncio = @idAnuncio";
    using var command = new SqlCommand(query, connection);
    command.Parameters.AddWithValue("@idAnuncio", idAnuncio);
    using var reader = await command.ExecuteReaderAsync();
    while (await reader.ReadAsync()) {
      results.Add(new
        {
          idTransacao     = reader.GetInt32(0),
          idAnuncio       = reader.GetInt32(1),
          idUserComprador = reader.GetInt32(2),
          estado          = reader.GetString(3),
          dataInicio      = reader.GetDateTime(4),
          dataFimPrevista = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5),
          dataFimEfetiva  = reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6),
          observacoes     = reader.IsDBNull(7) ? null : reader.GetString(7)
        });
      }
      return results;
  }
  public async Task<List<object>> GetTransacoesByComprador(int idUserComprador) {
    var results = new List<object>();
    using var connection = new SqlConnection(_connectionString);
    await connection.OpenAsync();
    var query = @"SELECT * FROM BookBridge.dbo.Transacao WHERE idUserComprador = @idUserComprador";
    using var command = new SqlCommand(query, connection);
    command.Parameters.AddWithValue("@idUserComprador", idUserComprador);
    using var reader = await command.ExecuteReaderAsync();
    while (await reader.ReadAsync()) {
      results.Add(new
        {
          idTransacao     = reader.GetInt32(0),
          idAnuncio       = reader.GetInt32(1),
          idUserComprador = reader.GetInt32(2),
          estado          = reader.GetString(3),
          dataInicio      = reader.GetDateTime(4),
          dataFimPrevista = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5),
          dataFimEfetiva  = reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6),
          observacoes     = reader.IsDBNull(7) ? null : reader.GetString(7)
        });
      }
      return results;
  }
  public async Task InsertTransacao(int idAnuncio, int idUserComprador, DateTime? dataFimPrevista, string? observacoes) {
    using var connection = new SqlConnection(_connectionString);
    await connection.OpenAsync();
    var query = @"INSERT INTO BookBridge.dbo.Transacao 
                  (idAnuncio, idUserComprador, dataFimPrevista, observacoes)
                  VALUES (@idAnuncio, @idUserComprador, @dataFimPrevista, @observacoes)";
    using var command = new SqlCommand(query, connection);
    command.Parameters.AddWithValue("@idAnuncio",       idAnuncio);
    command.Parameters.AddWithValue("@idUserComprador", idUserComprador);
    command.Parameters.AddWithValue("@dataFimPrevista", (object?)dataFimPrevista ?? DBNull.Value);
    command.Parameters.AddWithValue("@observacoes",     (object?)observacoes     ?? DBNull.Value);
    await command.ExecuteNonQueryAsync();
  }
  public async Task UpdateTransacao(int idTransacao, string estado, DateTime? dataFimPrevista, DateTime? dataFimEfetiva, string? observacoes) {
    using var connection = new SqlConnection(_connectionString);
    await connection.OpenAsync();
    var query = @"UPDATE BookBridge.dbo.Transacao
                  SET estado          = @estado,
                      dataFimPrevista = @dataFimPrevista,
                      dataFimEfetiva  = @dataFimEfetiva,
                      observacoes     = @observacoes
                  WHERE idTransacao = @idTransacao";
    using var command = new SqlCommand(query, connection);
    command.Parameters.AddWithValue("@idTransacao",     idTransacao);
    command.Parameters.AddWithValue("@estado",          estado);
    command.Parameters.AddWithValue("@dataFimPrevista", (object?)dataFimPrevista ?? DBNull.Value);
    command.Parameters.AddWithValue("@dataFimEfetiva",  (object?)dataFimEfetiva  ?? DBNull.Value);
    command.Parameters.AddWithValue("@observacoes",     (object?)observacoes     ?? DBNull.Value);
    await command.ExecuteNonQueryAsync();
  }
  public async Task DeleteTransacao(int idTransacao) {
    using var connection = new SqlConnection(_connectionString);
    await connection.OpenAsync();
    var query = @"DELETE FROM BookBridge.dbo.Transacao WHERE idTransacao = @idTransacao";
    using var command = new SqlCommand(query, connection);
    command.Parameters.AddWithValue("@idTransacao", idTransacao);
    await command.ExecuteNonQueryAsync();
  }

  public async Task<int> GetDiasTransacao(int idTransacao)
  {
      using var connection = new SqlConnection(_connectionString);
      await connection.OpenAsync();

      var query = "SELECT dbo.fn_DiasTransacao(@idTransacao)";

      using var command = new SqlCommand(query, connection);
      command.Parameters.AddWithValue("@idTransacao", idTransacao);

      var result = await command.ExecuteScalarAsync();

      return Convert.ToInt32(result);
  }

  public async Task<List<object>> GetTransacoesAtivas()
  {
      var results = new List<object>();

      using var connection = new SqlConnection(_connectionString);
      await connection.OpenAsync();

      var query = "SELECT * FROM dbo.vw_TransacoesAtivas";

      using var command = new SqlCommand(query, connection);
      using var reader = await command.ExecuteReaderAsync();

      while (await reader.ReadAsync())
      {
          results.Add(new
          {
              idTransacao = reader.GetInt32(0),
              tipoAnuncio = reader.GetString(1),
              nomeComprador = reader.GetString(2),
              nomeVendedor = reader.GetString(3),
              titulo = reader.GetString(4),
              estado = reader.GetString(5),
              dataInicio = reader.GetDateTime(6),
              dataFimPrevista = reader.IsDBNull(7)
                  ? (DateTime?)null
                  : reader.GetDateTime(7)
          });
      }

      return results;
  }

  public async Task<object?> GetIndicadores()
  {
      using var connection = new SqlConnection(_connectionString);
      await connection.OpenAsync();

      var query = "SELECT * FROM dbo.vw_Indicadores";

      using var command = new SqlCommand(query, connection);
      using var reader = await command.ExecuteReaderAsync();

      if (await reader.ReadAsync())
      {
          return new
          {
              anunciosAtivos = reader.GetInt32(0),
              totalLivros = reader.GetInt32(1),
              utilizadoresAtivos = reader.GetInt32(2),
              transacoesConcluidas = reader.GetInt32(3),
              transacoesPendentes = reader.GetInt32(4),
              totalTransacoes = reader.GetInt32(5),
              pedidosAtivos = reader.GetInt32(6),
              taxaSucessoPct = reader.IsDBNull(7)
                  ? 0m
                  : reader.GetDecimal(7)
          };
      }

      return null;
  }

  public async Task<int> IniciarTransacao(
    int idAnuncio,
    int idUserComprador,
    DateTime? dataFimPrevista)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        using var command = new SqlCommand("dbo.sp_IniciarTransacao", connection);
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@idAnuncio", idAnuncio);
        command.Parameters.AddWithValue("@idUserComprador", idUserComprador);
        command.Parameters.AddWithValue("@dataFimPrevista", (object?)dataFimPrevista ?? DBNull.Value);

        var output = new SqlParameter("@idTransacao", SqlDbType.Int)
        {
            Direction = ParameterDirection.Output
        };

        command.Parameters.Add(output);

        await command.ExecuteNonQueryAsync();

        return (int)output.Value;
    }

  public async Task AtualizarTransacao(
  int idTransacao,
  string novoEstado,
  DateTime? dataFimEfetiva)
  {
      using var connection = new SqlConnection(_connectionString);
      await connection.OpenAsync();

      using var command = new SqlCommand("dbo.sp_AtualizarTransacao", connection);
      command.CommandType = CommandType.StoredProcedure;

      command.Parameters.AddWithValue("@idTransacao", idTransacao);
      command.Parameters.AddWithValue("@novoEstado", novoEstado);
      command.Parameters.AddWithValue("@dataFimEfetiva", (object?)dataFimEfetiva ?? DBNull.Value);

      await command.ExecuteNonQueryAsync();
  }
  }
