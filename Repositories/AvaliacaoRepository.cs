using Microsoft.Data.SqlClient;

public class AvaliacaoRepository : IAvaliacaoRepository
{
    private readonly string _connectionString;

    public AvaliacaoRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
          ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
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
                idAvaliacao     = reader.GetInt32(0),
                idUserAvaliado  = reader.GetInt32(1),
                idUserAvaliador = reader.GetInt32(2),
                idHistorico     = reader.GetInt32(3),
                rating          = reader.GetInt32(4),
                comentario      = reader.IsDBNull(5) ? null : reader.GetString(5),
                dataAvaliacao   = DateOnly.FromDateTime(reader.GetDateTime(6))
            });
        }
        return results;
    }

    public async Task<object?> GetAvaliacaoById(int idAvaliacao)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"SELECT * FROM BookBridge.dbo.Avaliacao WHERE idAvaliacao = @idAvaliacao";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@idAvaliacao", idAvaliacao);
        using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new
            {
                idAvaliacao     = reader.GetInt32(0),
                idUserAvaliado  = reader.GetInt32(1),
                idUserAvaliador = reader.GetInt32(2),
                idHistorico     = reader.GetInt32(3),
                rating          = reader.GetInt32(4),
                comentario      = reader.IsDBNull(5) ? null : reader.GetString(5),
                dataAvaliacao   = DateOnly.FromDateTime(reader.GetDateTime(6))
            };
        }
        return null;
    }

    // dataAvaliacao → DEFAULT GETDATE(), gerado pela BD
    // rating aceita: 1 a 5 (CHECK constraint na BD)
    // comentario é opcional
    // BD garante unicidade: um avaliador só pode avaliar uma vez por histotico (UK_Avaliacao_unica)
    public async Task InsertAvaliacao(int idUserAvaliado, int idUserAvaliador, int idHistorico, int rating, string? comentario)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"INSERT INTO BookBridge.dbo.Avaliacao
                          (idUserAvaliado, idUserAvaliador, idHistorico, rating, comentario)
                      VALUES
                          (@idUserAvaliado, @idUserAvaliador, @idHistorico, @rating, @comentario)";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@idUserAvaliado",  idUserAvaliado);
        command.Parameters.AddWithValue("@idUserAvaliador", idUserAvaliador);
        command.Parameters.AddWithValue("@idHistorico",     idHistorico);
        command.Parameters.AddWithValue("@rating",          rating);
        command.Parameters.AddWithValue("@comentario",      (object?)comentario ?? DBNull.Value);
        await command.ExecuteNonQueryAsync();
    }

    // UPDATE apenas permite alterar rating e comentario
    public async Task UpdateAvaliacao(int idAvaliacao, int rating, string? comentario)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"UPDATE BookBridge.dbo.Avaliacao
                      SET rating     = @rating,
                          comentario = @comentario
                      WHERE idAvaliacao = @idAvaliacao";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@idAvaliacao", idAvaliacao);
        command.Parameters.AddWithValue("@rating",      rating);
        command.Parameters.AddWithValue("@comentario",  (object?)comentario ?? DBNull.Value);
        await command.ExecuteNonQueryAsync();
    }

    public async Task DeleteAvaliacao(int idAvaliacao)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"DELETE FROM BookBridge.dbo.Avaliacao WHERE idAvaliacao = @idAvaliacao";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@idAvaliacao", idAvaliacao);
        await command.ExecuteNonQueryAsync();
    }
}
