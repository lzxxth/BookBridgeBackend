using Microsoft.Data.SqlClient;

public class ExemplarRepository : IExemplarRepository
{
    private readonly string _connectionString;

    public ExemplarRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
          ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    }

    public async Task<List<object>> GetExemplares()
    {
        var results = new List<object>();
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"SELECT * FROM BookBridge.dbo.Exemplar";
        using var command = new SqlCommand(query, connection);
        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            results.Add(new
            {
                idExemplar     = reader.GetInt32(0),
                idLivro        = reader.GetInt32(1),
                idProprietario = reader.GetInt32(2),
                estado         = reader.GetString(3),
                disponivel     = reader.GetBoolean(4),
                dataAdicionado = DateOnly.FromDateTime(reader.GetDateTime(5)),
                descricao      = reader.IsDBNull(6) ? null : reader.GetString(6)
            });
        }
        return results;
    }

    public async Task<object?> GetExemplarById(int idExemplar)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"SELECT * FROM BookBridge.dbo.Exemplar WHERE idExemplar = @idExemplar";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@idExemplar", idExemplar);
        using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new
            {
                idExemplar     = reader.GetInt32(0),
                idLivro        = reader.GetInt32(1),
                idProprietario = reader.GetInt32(2),
                estado         = reader.GetString(3),
                disponivel     = reader.GetBoolean(4),
                dataAdicionado = DateOnly.FromDateTime(reader.GetDateTime(5)),
                descricao      = reader.IsDBNull(6) ? null : reader.GetString(6)
            };
        }
        return null;
    }

    // dataAdicionado → DEFAULT GETDATE(), gerado pela BD
    // estado aceita: 'novo', 'bom', 'usado', 'danificado'
    public async Task InsertExemplar(int idLivro, int idProprietario, string estado, bool disponivel, string? descricao)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"INSERT INTO BookBridge.dbo.Exemplar
                          (idLivro, idProprietario, estado, disponivel, descricao)
                      VALUES
                          (@idLivro, @idProprietario, @estado, @disponivel, @descricao)";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@idLivro",        idLivro);
        command.Parameters.AddWithValue("@idProprietario", idProprietario);
        command.Parameters.AddWithValue("@estado",         estado);
        command.Parameters.AddWithValue("@disponivel",     disponivel);
        command.Parameters.AddWithValue("@descricao",      (object?)descricao ?? DBNull.Value);
        await command.ExecuteNonQueryAsync();
    }

    public async Task UpdateExemplar(int idExemplar, int idLivro, int idProprietario, string estado, bool disponivel, string? descricao)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"UPDATE BookBridge.dbo.Exemplar
                      SET idLivro        = @idLivro,
                          idProprietario = @idProprietario,
                          estado         = @estado,
                          disponivel     = @disponivel,
                          descricao      = @descricao
                      WHERE idExemplar = @idExemplar";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@idExemplar",     idExemplar);
        command.Parameters.AddWithValue("@idLivro",        idLivro);
        command.Parameters.AddWithValue("@idProprietario", idProprietario);
        command.Parameters.AddWithValue("@estado",         estado);
        command.Parameters.AddWithValue("@disponivel",     disponivel);
        command.Parameters.AddWithValue("@descricao",      (object?)descricao ?? DBNull.Value);
        await command.ExecuteNonQueryAsync();
    }

    public async Task DeleteExemplar(int idExemplar)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"DELETE FROM BookBridge.dbo.Exemplar WHERE idExemplar = @idExemplar";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@idExemplar", idExemplar);
        await command.ExecuteNonQueryAsync();
    }
}
