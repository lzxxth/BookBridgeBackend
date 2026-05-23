using Microsoft.Data.SqlClient;

public class DisciplinaRepository : IDisciplinaRepository
{
    private readonly string _connectionString;

    public DisciplinaRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
          ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    }

    public async Task<List<object>> GetDisciplinas()
    {
        var results = new List<object>();
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"SELECT * FROM BookBridge.dbo.Disciplina";
        using var command = new SqlCommand(query, connection);
        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            results.Add(new
            {
                idDisciplina = reader.GetInt32(0),
                nome         = reader.GetString(1),
                curso        = reader.IsDBNull(2) ? null : reader.GetString(2)
            });
        }
        return results;
    }

    public async Task<object?> GetDisciplinaById(int idDisciplina)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"SELECT * FROM BookBridge.dbo.Disciplina WHERE idDisciplina = @idDisciplina";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@idDisciplina", idDisciplina);
        using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new
            {
                idDisciplina = reader.GetInt32(0),
                nome         = reader.GetString(1),
                curso        = reader.IsDBNull(2) ? null : reader.GetString(2)
            };
        }
        return null;
    }

    // curso é opcional (NULL na BD)
    public async Task InsertDisciplina(string nome, string? curso)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"INSERT INTO BookBridge.dbo.Disciplina (nome, curso)
                      VALUES (@nome, @curso)";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@nome",  nome);
        command.Parameters.AddWithValue("@curso", (object?)curso ?? DBNull.Value);
        await command.ExecuteNonQueryAsync();
    }

    public async Task UpdateDisciplina(int idDisciplina, string nome, string? curso)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"UPDATE BookBridge.dbo.Disciplina
                      SET nome  = @nome,
                          curso = @curso
                      WHERE idDisciplina = @idDisciplina";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@idDisciplina", idDisciplina);
        command.Parameters.AddWithValue("@nome",         nome);
        command.Parameters.AddWithValue("@curso",        (object?)curso ?? DBNull.Value);
        await command.ExecuteNonQueryAsync();
    }

    public async Task DeleteDisciplina(int idDisciplina)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"DELETE FROM BookBridge.dbo.Disciplina WHERE idDisciplina = @idDisciplina";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@idDisciplina", idDisciplina);
        await command.ExecuteNonQueryAsync();
    }
}
