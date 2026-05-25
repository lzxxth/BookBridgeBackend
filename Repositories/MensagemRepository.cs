using Microsoft.Data.SqlClient;

public class MensagemRepository : IMensagemRepository
{
    private readonly string _connectionString;

    public MensagemRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException(
                "Connection string 'DefaultConnection' not found.");
    }

    public async Task<List<object>> GetMensagens()
    {
        var results = new List<object>();

        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var query = @"SELECT * FROM BookBridge.dbo.Mensagem";

        using var command = new SqlCommand(query, connection);
        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            results.Add(new
            {
                idMensagem = reader.GetInt32(0),
                idRemetente = reader.GetInt32(1),
                idDestinatario = reader.GetInt32(2),
                idAnuncio = reader.IsDBNull(3)
                    ? (int?)null
                    : reader.GetInt32(3),
                conteudo = reader.GetString(4),
                dataEnvio = reader.GetDateTime(5),
                lida = reader.GetBoolean(6)
            });
        }

        return results;
    }

    public async Task<object?> GetMensagemById(int idMensagem)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var query = @"SELECT * 
                      FROM BookBridge.dbo.Mensagem
                      WHERE idMensagem = @idMensagem";

        using var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@idMensagem", idMensagem);

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            return new
            {
                idMensagem = reader.GetInt32(0),
                idRemetente = reader.GetInt32(1),
                idDestinatario = reader.GetInt32(2),
                idAnuncio = reader.IsDBNull(3)
                    ? (int?)null
                    : reader.GetInt32(3),
                conteudo = reader.GetString(4),
                dataEnvio = reader.GetDateTime(5),
                lida = reader.GetBoolean(6)
            };
        }

        return null;
    }

    public async Task InsertMensagem(
        int idRemetente,
        int idDestinatario,
        int? idAnuncio,
        string conteudo)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var query = @"INSERT INTO BookBridge.dbo.Mensagem
                      (idRemetente, idDestinatario, idAnuncio, conteudo)
                      VALUES
                      (@idRemetente, @idDestinatario, @idAnuncio, @conteudo)";

        using var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@idRemetente", idRemetente);
        command.Parameters.AddWithValue("@idDestinatario", idDestinatario);
        command.Parameters.AddWithValue(
            "@idAnuncio",
            (object?)idAnuncio ?? DBNull.Value);
        command.Parameters.AddWithValue("@conteudo", conteudo);

        await command.ExecuteNonQueryAsync();
    }

    public async Task UpdateMensagem(
        int idMensagem,
        int idRemetente,
        int idDestinatario,
        int? idAnuncio,
        string conteudo,
        bool lida)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var query = @"UPDATE BookBridge.dbo.Mensagem
                      SET idRemetente = @idRemetente,
                          idDestinatario = @idDestinatario,
                          idAnuncio = @idAnuncio,
                          conteudo = @conteudo,
                          lida = @lida
                      WHERE idMensagem = @idMensagem";

        using var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@idMensagem", idMensagem);
        command.Parameters.AddWithValue("@idRemetente", idRemetente);
        command.Parameters.AddWithValue("@idDestinatario", idDestinatario);
        command.Parameters.AddWithValue(
            "@idAnuncio",
            (object?)idAnuncio ?? DBNull.Value);
        command.Parameters.AddWithValue("@conteudo", conteudo);
        command.Parameters.AddWithValue("@lida", lida);

        await command.ExecuteNonQueryAsync();
    }

    public async Task DeleteMensagem(int idMensagem)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var query = @"DELETE FROM BookBridge.dbo.Mensagem
                      WHERE idMensagem = @idMensagem";

        using var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@idMensagem", idMensagem);

        await command.ExecuteNonQueryAsync();
    }
}