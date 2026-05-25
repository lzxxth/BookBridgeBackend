using Microsoft.Data.SqlClient;

public class HistoricoRepository : IHistoricoRepository
{
    private readonly string _connectionString;

    public HistoricoRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException(
                "Connection string 'DefaultConnection' not found.");
    }

    public async Task<List<object>> GetHistoricos()
    {
        var results = new List<object>();

        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var query = @"SELECT * FROM BookBridge.dbo.Historico";

        using var command = new SqlCommand(query, connection);
        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            results.Add(new
            {
                idHistorico = reader.GetInt32(0),
                idTransacao = reader.GetInt32(1),
                idUser = reader.GetInt32(2),
                descricao = reader.IsDBNull(3)
                    ? null
                    : reader.GetString(3),
                dataRegisto = reader.GetDateTime(4)
            });
        }

        return results;
    }

    public async Task<object?> GetHistoricoById(int idHistorico)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var query = @"SELECT * 
                      FROM BookBridge.dbo.Historico
                      WHERE idHistorico = @idHistorico";

        using var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@idHistorico", idHistorico);

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            return new
            {
                idHistorico = reader.GetInt32(0),
                idTransacao = reader.GetInt32(1),
                idUser = reader.GetInt32(2),
                descricao = reader.IsDBNull(3)
                    ? null
                    : reader.GetString(3),
                dataRegisto = reader.GetDateTime(4)
            };
        }

        return null;
    }

    public async Task InsertHistorico(
        int idTransacao,
        int idUser,
        string? descricao)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var query = @"INSERT INTO BookBridge.dbo.Historico
                      (idTransacao, idUser, descricao)
                      VALUES
                      (@idTransacao, @idUser, @descricao)";

        using var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@idTransacao", idTransacao);
        command.Parameters.AddWithValue("@idUser", idUser);
        command.Parameters.AddWithValue(
            "@descricao",
            (object?)descricao ?? DBNull.Value);

        await command.ExecuteNonQueryAsync();
    }

    public async Task UpdateHistorico(
        int idHistorico,
        int idTransacao,
        int idUser,
        string? descricao)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var query = @"UPDATE BookBridge.dbo.Historico
                      SET idTransacao = @idTransacao,
                          idUser = @idUser,
                          descricao = @descricao
                      WHERE idHistorico = @idHistorico";

        using var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@idHistorico", idHistorico);
        command.Parameters.AddWithValue("@idTransacao", idTransacao);
        command.Parameters.AddWithValue("@idUser", idUser);
        command.Parameters.AddWithValue(
            "@descricao",
            (object?)descricao ?? DBNull.Value);

        await command.ExecuteNonQueryAsync();
    }

    public async Task DeleteHistorico(int idHistorico)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var query = @"DELETE FROM BookBridge.dbo.Historico
                      WHERE idHistorico = @idHistorico";

        using var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@idHistorico", idHistorico);

        await command.ExecuteNonQueryAsync();
    }
    public async Task<List<object>> GetHistoricoDetalhado()
    {
        var results = new List<object>();

        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var query = "SELECT * FROM dbo.vw_HistoricoDetalhado";

        using var command = new SqlCommand(query, connection);
        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            results.Add(new
            {
                idHistorico = reader.GetInt32(0),
                utilizador = reader.GetString(1),
                estadoTransacao = reader.GetString(2),
                tipoAnuncio = reader.GetString(3),
                tituloLivro = reader.GetString(4),
                dataInicio = reader.GetDateTime(5),
                dataFimEfetiva = reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6),
                dataRegisto = reader.GetDateTime(7)
            });
        }

        return results;
    }
}