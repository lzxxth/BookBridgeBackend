using Microsoft.Data.SqlClient;

public class MensagemRepository : IMensagemRepository
{
    private readonly string _connectionString;

    public MensagemRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
          ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
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
                idMensagem     = reader.GetInt32(0),
                idRemetente    = reader.GetInt32(1),
                idDestinatario = reader.GetInt32(2),
                idHistorico    = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3),
                conteudo       = reader.GetString(4),
                dataEnvio      = reader.GetDateTime(5),   // DATETIME → mantém hora
                lida           = reader.GetBoolean(6)
            });
        }
        return results;
    }

    public async Task<object?> GetMensagemById(int idMensagem)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"SELECT * FROM BookBridge.dbo.Mensagem WHERE idMensagem = @idMensagem";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@idMensagem", idMensagem);
        using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new
            {
                idMensagem     = reader.GetInt32(0),
                idRemetente    = reader.GetInt32(1),
                idDestinatario = reader.GetInt32(2),
                idHistorico    = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3),
                conteudo       = reader.GetString(4),
                dataEnvio      = reader.GetDateTime(5),
                lida           = reader.GetBoolean(6)
            };
        }
        return null;
    }

    // dataEnvio → DEFAULT GETDATE(), gerado pela BD
    // lida      → DEFAULT 0, gerado pela BD
    // idHistorico é opcional (NULL na BD)
    public async Task InsertMensagem(int idRemetente, int idDestinatario, int? idHistorico, string conteudo)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"INSERT INTO BookBridge.dbo.Mensagem
                          (idRemetente, idDestinatario, idHistorico, conteudo)
                      VALUES
                          (@idRemetente, @idDestinatario, @idHistorico, @conteudo)";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@idRemetente",    idRemetente);
        command.Parameters.AddWithValue("@idDestinatario", idDestinatario);
        command.Parameters.AddWithValue("@idHistorico",    (object?)idHistorico ?? DBNull.Value);
        command.Parameters.AddWithValue("@conteudo",       conteudo);
        await command.ExecuteNonQueryAsync();
    }

    // UPDATE apenas atualiza o campo lida (marcar como lida/não lida)
    public async Task UpdateMensagem(int idMensagem, bool lida)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"UPDATE BookBridge.dbo.Mensagem
                      SET lida = @lida
                      WHERE idMensagem = @idMensagem";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@idMensagem", idMensagem);
        command.Parameters.AddWithValue("@lida",       lida);
        await command.ExecuteNonQueryAsync();
    }

    public async Task DeleteMensagem(int idMensagem)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"DELETE FROM BookBridge.dbo.Mensagem WHERE idMensagem = @idMensagem";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@idMensagem", idMensagem);
        await command.ExecuteNonQueryAsync();
    }
}
