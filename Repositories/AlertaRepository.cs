using Microsoft.Data.SqlClient;

public class AlertaRepository : IAlertaRepository
{
    private readonly string _connectionString;

    public AlertaRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException(
                "Connection string 'DefaultConnection' not found.");
    }

    public async Task<List<object>> GetAlertas()
    {
        var results = new List<object>();

        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var query = @"SELECT * FROM BookBridge.dbo.Alerta";

        using var command = new SqlCommand(query, connection);
        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            results.Add(new
            {
                idAlerta = reader.GetInt32(0),
                idUser = reader.GetInt32(1),
                idAnuncio = reader.IsDBNull(2)
                    ? (int?)null
                    : reader.GetInt32(2),
                idPedido = reader.IsDBNull(3)
                    ? (int?)null
                    : reader.GetInt32(3),
                mensagem = reader.GetString(4),
                lido = reader.GetBoolean(5),
                dataAlerta = reader.GetDateTime(6)
            });
        }

        return results;
    }

    public async Task<object?> GetAlertaById(int idAlerta)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var query = @"SELECT * 
                      FROM BookBridge.dbo.Alerta
                      WHERE idAlerta = @idAlerta";

        using var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@idAlerta", idAlerta);

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            return new
            {
                idAlerta = reader.GetInt32(0),
                idUser = reader.GetInt32(1),
                idAnuncio = reader.IsDBNull(2)
                    ? (int?)null
                    : reader.GetInt32(2),
                idPedido = reader.IsDBNull(3)
                    ? (int?)null
                    : reader.GetInt32(3),
                mensagem = reader.GetString(4),
                lido = reader.GetBoolean(5),
                dataAlerta = reader.GetDateTime(6)
            };
        }

        return null;
    }

    public async Task InsertAlerta(
        int idUser,
        int? idAnuncio,
        int? idPedido,
        string mensagem)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var query = @"INSERT INTO BookBridge.dbo.Alerta
                      (idUser, idAnuncio, idPedido, mensagem)
                      VALUES
                      (@idUser, @idAnuncio, @idPedido, @mensagem)";

        using var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@idUser", idUser);
        command.Parameters.AddWithValue(
            "@idAnuncio",
            (object?)idAnuncio ?? DBNull.Value);
        command.Parameters.AddWithValue(
            "@idPedido",
            (object?)idPedido ?? DBNull.Value);
        command.Parameters.AddWithValue("@mensagem", mensagem);

        await command.ExecuteNonQueryAsync();
    }

    public async Task UpdateAlerta(
        int idAlerta,
        int idUser,
        int? idAnuncio,
        int? idPedido,
        string mensagem,
        bool lido)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var query = @"UPDATE BookBridge.dbo.Alerta
                      SET idUser = @idUser,
                          idAnuncio = @idAnuncio,
                          idPedido = @idPedido,
                          mensagem = @mensagem,
                          lido = @lido
                      WHERE idAlerta = @idAlerta";

        using var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@idAlerta", idAlerta);
        command.Parameters.AddWithValue("@idUser", idUser);
        command.Parameters.AddWithValue(
            "@idAnuncio",
            (object?)idAnuncio ?? DBNull.Value);
        command.Parameters.AddWithValue(
            "@idPedido",
            (object?)idPedido ?? DBNull.Value);
        command.Parameters.AddWithValue("@mensagem", mensagem);
        command.Parameters.AddWithValue("@lido", lido);

        await command.ExecuteNonQueryAsync();
    }

    public async Task DeleteAlerta(int idAlerta)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var query = @"DELETE FROM BookBridge.dbo.Alerta
                      WHERE idAlerta = @idAlerta";

        using var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@idAlerta", idAlerta);

        await command.ExecuteNonQueryAsync();
    }
}