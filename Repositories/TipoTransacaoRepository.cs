using Microsoft.Data.SqlClient;

public class TipoTransacaoRepository : ITipoTransacaoRepository
{
    private readonly string _connectionString;

    public TipoTransacaoRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
          ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    }

    public async Task<List<object>> GetTipoTransacoes()
    {
        var results = new List<object>();
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"SELECT * FROM BookBridge.dbo.TipoTransacao";
        using var command = new SqlCommand(query, connection);
        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            results.Add(new
            {
                idTipoTransacao = reader.GetInt32(0),
                descricao       = reader.GetString(1)
            });
        }
        return results;
    }

    public async Task<object?> GetTipoTransacaoById(int idTipoTransacao)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"SELECT * FROM BookBridge.dbo.TipoTransacao WHERE idTipoTransacao = @idTipoTransacao";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@idTipoTransacao", idTipoTransacao);
        using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new
            {
                idTipoTransacao = reader.GetInt32(0),
                descricao       = reader.GetString(1)
            };
        }
        return null;
    }

    public async Task InsertTipoTransacao(string descricao)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"INSERT INTO BookBridge.dbo.TipoTransacao (descricao) VALUES (@descricao)";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@descricao", descricao);
        await command.ExecuteNonQueryAsync();
    }

    public async Task UpdateTipoTransacao(int idTipoTransacao, string descricao)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"UPDATE BookBridge.dbo.TipoTransacao
                      SET descricao = @descricao
                      WHERE idTipoTransacao = @idTipoTransacao";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@idTipoTransacao", idTipoTransacao);
        command.Parameters.AddWithValue("@descricao",       descricao);
        await command.ExecuteNonQueryAsync();
    }

    public async Task DeleteTipoTransacao(int idTipoTransacao)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"DELETE FROM BookBridge.dbo.TipoTransacao WHERE idTipoTransacao = @idTipoTransacao";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@idTipoTransacao", idTipoTransacao);
        await command.ExecuteNonQueryAsync();
    }
}
