using Microsoft.Data.SqlClient;

public class HistoricoRepository : IHistoricoRepository
{
    private readonly string _connectionString;

    public HistoricoRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
          ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
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
                idHistorico        = reader.GetInt32(0),
                idTipoTransacao    = reader.GetInt32(1),
                idUserSolicitante  = reader.GetInt32(2),
                idUserProprietario = reader.GetInt32(3),
                idExemplar         = reader.GetInt32(4),
                estado             = reader.GetString(5),
                dataRegisto        = DateOnly.FromDateTime(reader.GetDateTime(6)),
                dataConclusao      = reader.IsDBNull(7) ? (DateOnly?)null : DateOnly.FromDateTime(reader.GetDateTime(7)),
                observacoes        = reader.IsDBNull(8) ? null : reader.GetString(8)
            });
        }
        return results;
    }

    public async Task<object?> GetHistoricoById(int idHistorico)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"SELECT * FROM BookBridge.dbo.Historico WHERE idHistorico = @idHistorico";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@idHistorico", idHistorico);
        using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new
            {
                idHistorico        = reader.GetInt32(0),
                idTipoTransacao    = reader.GetInt32(1),
                idUserSolicitante  = reader.GetInt32(2),
                idUserProprietario = reader.GetInt32(3),
                idExemplar         = reader.GetInt32(4),
                estado             = reader.GetString(5),
                dataRegisto        = DateOnly.FromDateTime(reader.GetDateTime(6)),
                dataConclusao      = reader.IsDBNull(7) ? (DateOnly?)null : DateOnly.FromDateTime(reader.GetDateTime(7)),
                observacoes        = reader.IsDBNull(8) ? null : reader.GetString(8)
            };
        }
        return null;
    }

    // dataRegisto  → DEFAULT GETDATE(), gerado pela BD
    // dataConclusao e observacoes → opcionais (NULL na BD)
    // estado aceita: 'pendente', 'ativa', 'concluida', 'cancelada', 'recusada'
    public async Task InsertHistorico(int idTipoTransacao, int idUserSolicitante, int idUserProprietario, int idExemplar, string estado, string? observacoes)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"INSERT INTO BookBridge.dbo.Historico
                          (idTipoTransacao, idUserSolicitante, idUserProprietario, idExemplar, estado, observacoes)
                      VALUES
                          (@idTipoTransacao, @idUserSolicitante, @idUserProprietario, @idExemplar, @estado, @observacoes)";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@idTipoTransacao",    idTipoTransacao);
        command.Parameters.AddWithValue("@idUserSolicitante",  idUserSolicitante);
        command.Parameters.AddWithValue("@idUserProprietario", idUserProprietario);
        command.Parameters.AddWithValue("@idExemplar",         idExemplar);
        command.Parameters.AddWithValue("@estado",             estado);
        command.Parameters.AddWithValue("@observacoes",        (object?)observacoes ?? DBNull.Value);
        await command.ExecuteNonQueryAsync();
    }

    public async Task UpdateHistorico(int idHistorico, int idTipoTransacao, int idUserSolicitante, int idUserProprietario, int idExemplar, string estado, DateOnly? dataConclusao, string? observacoes)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"UPDATE BookBridge.dbo.Historico
                      SET idTipoTransacao    = @idTipoTransacao,
                          idUserSolicitante  = @idUserSolicitante,
                          idUserProprietario = @idUserProprietario,
                          idExemplar         = @idExemplar,
                          estado             = @estado,
                          dataConclusao      = @dataConclusao,
                          observacoes        = @observacoes
                      WHERE idHistorico = @idHistorico";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@idHistorico",        idHistorico);
        command.Parameters.AddWithValue("@idTipoTransacao",    idTipoTransacao);
        command.Parameters.AddWithValue("@idUserSolicitante",  idUserSolicitante);
        command.Parameters.AddWithValue("@idUserProprietario", idUserProprietario);
        command.Parameters.AddWithValue("@idExemplar",         idExemplar);
        command.Parameters.AddWithValue("@estado",             estado);
        command.Parameters.AddWithValue("@dataConclusao",      dataConclusao.HasValue ? (object)dataConclusao.Value.ToDateTime(TimeOnly.MinValue) : DBNull.Value);
        command.Parameters.AddWithValue("@observacoes",        (object?)observacoes ?? DBNull.Value);
        await command.ExecuteNonQueryAsync();
    }

    public async Task DeleteHistorico(int idHistorico)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"DELETE FROM BookBridge.dbo.Historico WHERE idHistorico = @idHistorico";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@idHistorico", idHistorico);
        await command.ExecuteNonQueryAsync();
    }
}
