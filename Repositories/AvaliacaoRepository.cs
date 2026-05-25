using Microsoft.Data.SqlClient;
using System.Data;

public class AvaliacaoRepository : IAvaliacaoRepository
{
    private readonly string _connectionString;

    public AvaliacaoRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException(
                "Connection string 'DefaultConnection' not found.");
    }

    public async Task<List<object>> GetAvaliacoes()
    {
        var results = new List<object>();

        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var query = @"SELECT * FROM BookBridge.dbo.Avaliacao";

        using var command = new SqlCommand(query, connection);
        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            results.Add(new
            {
                idAvaliacao = reader.GetInt32(0),
                idUserAvaliado = reader.GetInt32(1),
                idUserAvaliador = reader.GetInt32(2),
                idTransacao = reader.GetInt32(3),
                rating = reader.GetInt32(4),
                comentario = reader.IsDBNull(5)
                    ? null
                    : reader.GetString(5),
                dataAvaliacao = reader.GetDateTime(6)
            });
        }

        return results;
    }

    public async Task<object?> GetAvaliacaoById(int idAvaliacao)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var query = @"SELECT * 
                      FROM BookBridge.dbo.Avaliacao
                      WHERE idAvaliacao = @idAvaliacao";

        using var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@idAvaliacao", idAvaliacao);

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            return new
            {
                idAvaliacao = reader.GetInt32(0),
                idUserAvaliado = reader.GetInt32(1),
                idUserAvaliador = reader.GetInt32(2),
                idTransacao = reader.GetInt32(3),
                rating = reader.GetInt32(4),
                comentario = reader.IsDBNull(5)
                    ? null
                    : reader.GetString(5),
                dataAvaliacao = reader.GetDateTime(6)
            };
        }

        return null;
    }

    public async Task InsertAvaliacao(
        int idUserAvaliado,
        int idUserAvaliador,
        int idTransacao,
        int rating,
        string? comentario)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        using var command = new SqlCommand("dbo.sp_AvaliarUtilizador", connection);
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@idUserAvaliado", idUserAvaliado);
        command.Parameters.AddWithValue("@idUserAvaliador", idUserAvaliador);
        command.Parameters.AddWithValue("@idTransacao", idTransacao);
        command.Parameters.AddWithValue("@rating", rating);
        command.Parameters.AddWithValue(
            "@comentario",
            (object?)comentario ?? DBNull.Value);

        await command.ExecuteNonQueryAsync();
    }

    public async Task UpdateAvaliacao(
        int idAvaliacao,
        int idUserAvaliado,
        int idUserAvaliador,
        int idTransacao,
        int rating,
        string? comentario)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var query = @"UPDATE BookBridge.dbo.Avaliacao
                      SET idUserAvaliado = @idUserAvaliado,
                          idUserAvaliador = @idUserAvaliador,
                          idTransacao = @idTransacao,
                          rating = @rating,
                          comentario = @comentario
                      WHERE idAvaliacao = @idAvaliacao";

        using var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@idAvaliacao", idAvaliacao);
        command.Parameters.AddWithValue("@idUserAvaliado", idUserAvaliado);
        command.Parameters.AddWithValue("@idUserAvaliador", idUserAvaliador);
        command.Parameters.AddWithValue("@idTransacao", idTransacao);
        command.Parameters.AddWithValue("@rating", rating);
        command.Parameters.AddWithValue(
            "@comentario",
            (object?)comentario ?? DBNull.Value);

        await command.ExecuteNonQueryAsync();
    }

    public async Task DeleteAvaliacao(int idAvaliacao)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var query = @"DELETE FROM BookBridge.dbo.Avaliacao
                      WHERE idAvaliacao = @idAvaliacao";

        using var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@idAvaliacao", idAvaliacao);

        await command.ExecuteNonQueryAsync();
    }

    public async Task<decimal> GetMediaAvaliacao(int idUser)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var query = "SELECT dbo.fn_MediaAvaliacao(@idUser)";

        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@idUser", idUser);

        var result = await command.ExecuteScalarAsync();

        return Convert.ToDecimal(result);
    }
    public async Task<List<object>> GetAvaliacoesDetalhadas()
    {
        var results = new List<object>();

        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var query = "SELECT * FROM dbo.vw_AvaliacoesDetalhadas";

        using var command = new SqlCommand(query, connection);
        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            results.Add(new
            {
                idAvaliacao = reader.GetInt32(0),
                nomeAvaliador = reader.GetString(1),
                nomeAvaliado = reader.GetString(2),
                rating = reader.GetInt32(3),
                comentario = reader.IsDBNull(4) ? null : reader.GetString(4),
                dataAvaliacao = reader.GetDateTime(5),
                tipoAnuncio = reader.GetString(6)
            });
        }

        return results;
    }
}