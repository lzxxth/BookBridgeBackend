using Microsoft.Data.SqlClient;

public class TipoUserRepository : ITipoUserRepository
{
    private readonly string _connectionString;

    public TipoUserRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
          ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    }

    public async Task<List<object>> GetTipoUsers()
    {
        var results = new List<object>();
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"SELECT * FROM BookBridge.dbo.TipoUser";
        using var command = new SqlCommand(query, connection);
        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            results.Add(new
            {
                idTipoUser = reader.GetInt32(0),
                descricao  = reader.GetString(1)
            });
        }
        return results;
    }

    public async Task<object?> GetTipoUserById(int idTipoUser)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"SELECT * FROM BookBridge.dbo.TipoUser WHERE idTipoUser = @idTipoUser";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@idTipoUser", idTipoUser);
        using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new
            {
                idTipoUser = reader.GetInt32(0),
                descricao  = reader.GetString(1)
            };
        }
        return null;
    }

    public async Task InsertTipoUser(string descricao)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"INSERT INTO BookBridge.dbo.TipoUser (descricao)
                      VALUES (@descricao)";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@descricao", descricao);
        await command.ExecuteNonQueryAsync();
    }

    public async Task UpdateTipoUser(int idTipoUser, string descricao)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"UPDATE BookBridge.dbo.TipoUser
                      SET descricao = @descricao
                      WHERE idTipoUser = @idTipoUser";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@idTipoUser", idTipoUser);
        command.Parameters.AddWithValue("@descricao",  descricao);
        await command.ExecuteNonQueryAsync();
    }

    public async Task DeleteTipoUser(int idTipoUser)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"DELETE FROM BookBridge.dbo.TipoUser WHERE idTipoUser = @idTipoUser";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@idTipoUser", idTipoUser);
        await command.ExecuteNonQueryAsync();
    }
}
