using Microsoft.Data.SqlClient;

public class UserRepository : IUserRepository
{
    private readonly string _connectionString;

    public UserRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
          ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    }

    public async Task<List<object>> GetUsers()
    {
        var results = new List<object>();
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"SELECT * FROM BookBridge.dbo.[User]";
        using var command = new SqlCommand(query, connection);
        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            results.Add(new
            {
                idUser       = reader.GetInt32(0),
                nome         = reader.GetString(1),
                email        = reader.GetString(2),
                passwordHash = reader.GetString(3),
                idTipoUser   = reader.GetInt32(4),
                curso        = reader.IsDBNull(5) ? null : reader.GetString(5),
                instituicao  = reader.IsDBNull(6) ? null : reader.GetString(6),
                dataRegisto  = DateOnly.FromDateTime(reader.GetDateTime(7)),
                ativo        = reader.GetBoolean(8)
            });
        }
        return results;
    }

    public async Task<object?> GetUserById(int idUser)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"SELECT * FROM BookBridge.dbo.[User] WHERE idUser = @idUser";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@idUser", idUser);
        using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new
            {
                idUser       = reader.GetInt32(0),
                nome         = reader.GetString(1),
                email        = reader.GetString(2),
                passwordHash = reader.GetString(3),
                idTipoUser   = reader.GetInt32(4),
                curso        = reader.IsDBNull(5) ? null : reader.GetString(5),
                instituicao  = reader.IsDBNull(6) ? null : reader.GetString(6),
                dataRegisto  = DateOnly.FromDateTime(reader.GetDateTime(7)),
                ativo        = reader.GetBoolean(8)
            };
        }
        return null;
    }

    // dataRegisto → DEFAULT GETDATE(), gerado pela BD
    // ativo       → DEFAULT 1, mas passado explicitamente para permitir override
    public async Task InsertUser(string nome, string email, string passwordHash, int idTipoUser, string? curso, string? instituicao, bool ativo)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"INSERT INTO BookBridge.dbo.[User]
                          (nome, email, passwordHash, idTipoUser, curso, instituicao, ativo)
                      VALUES
                          (@nome, @email, @passwordHash, @idTipoUser, @curso, @instituicao, @ativo)";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@nome",         nome);
        command.Parameters.AddWithValue("@email",        email);
        command.Parameters.AddWithValue("@passwordHash", passwordHash);
        command.Parameters.AddWithValue("@idTipoUser",   idTipoUser);
        command.Parameters.AddWithValue("@curso",        (object?)curso        ?? DBNull.Value);
        command.Parameters.AddWithValue("@instituicao",  (object?)instituicao  ?? DBNull.Value);
        command.Parameters.AddWithValue("@ativo",        ativo);
        await command.ExecuteNonQueryAsync();
    }

    public async Task UpdateUser(int idUser, string nome, string email, string passwordHash, int idTipoUser, string? curso, string? instituicao, bool ativo)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"UPDATE BookBridge.dbo.[User]
                      SET nome         = @nome,
                          email        = @email,
                          passwordHash = @passwordHash,
                          idTipoUser   = @idTipoUser,
                          curso        = @curso,
                          instituicao  = @instituicao,
                          ativo        = @ativo
                      WHERE idUser = @idUser";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@idUser",       idUser);
        command.Parameters.AddWithValue("@nome",         nome);
        command.Parameters.AddWithValue("@email",        email);
        command.Parameters.AddWithValue("@passwordHash", passwordHash);
        command.Parameters.AddWithValue("@idTipoUser",   idTipoUser);
        command.Parameters.AddWithValue("@curso",        (object?)curso        ?? DBNull.Value);
        command.Parameters.AddWithValue("@instituicao",  (object?)instituicao  ?? DBNull.Value);
        command.Parameters.AddWithValue("@ativo",        ativo);
        await command.ExecuteNonQueryAsync();
    }

    public async Task DeleteUser(int idUser)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"DELETE FROM BookBridge.dbo.[User] WHERE idUser = @idUser";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@idUser", idUser);
        await command.ExecuteNonQueryAsync();
    }
}
