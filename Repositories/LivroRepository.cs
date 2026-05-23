using Microsoft.Data.SqlClient;

public class LivroRepository : ILivroRepository
{
    private readonly string _connectionString;

    public LivroRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
          ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    }

    public async Task<List<object>> GetLivros()
    {
        var results = new List<object>();
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"SELECT * FROM BookBridge.dbo.Livro";
        using var command = new SqlCommand(query, connection);
        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            results.Add(new
            {
                idLivro      = reader.GetInt32(0),
                titulo       = reader.GetString(1),
                isbn         = reader.IsDBNull(2) ? null : reader.GetString(2),
                idCategoria  = reader.GetInt32(3),
                idDisciplina = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4)
            });
        }
        return results;
    }

    public async Task<object?> GetLivroById(int idLivro)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"SELECT * FROM BookBridge.dbo.Livro WHERE idLivro = @idLivro";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@idLivro", idLivro);
        using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new
            {
                idLivro      = reader.GetInt32(0),
                titulo       = reader.GetString(1),
                isbn         = reader.IsDBNull(2) ? null : reader.GetString(2),
                idCategoria  = reader.GetInt32(3),
                idDisciplina = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4)
            };
        }
        return null;
    }

    // isbn e idDisciplina são opcionais (NULL na BD)
    public async Task InsertLivro(string titulo, string? isbn, int idCategoria, int? idDisciplina)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"INSERT INTO BookBridge.dbo.Livro
                          (titulo, isbn, idCategoria, idDisciplina)
                      VALUES
                          (@titulo, @isbn, @idCategoria, @idDisciplina)";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@titulo",       titulo);
        command.Parameters.AddWithValue("@isbn",         (object?)isbn         ?? DBNull.Value);
        command.Parameters.AddWithValue("@idCategoria",  idCategoria);
        command.Parameters.AddWithValue("@idDisciplina", (object?)idDisciplina ?? DBNull.Value);
        await command.ExecuteNonQueryAsync();
    }

    public async Task UpdateLivro(int idLivro, string titulo, string? isbn, int idCategoria, int? idDisciplina)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"UPDATE BookBridge.dbo.Livro
                      SET titulo       = @titulo,
                          isbn         = @isbn,
                          idCategoria  = @idCategoria,
                          idDisciplina = @idDisciplina
                      WHERE idLivro = @idLivro";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@idLivro",      idLivro);
        command.Parameters.AddWithValue("@titulo",       titulo);
        command.Parameters.AddWithValue("@isbn",         (object?)isbn         ?? DBNull.Value);
        command.Parameters.AddWithValue("@idCategoria",  idCategoria);
        command.Parameters.AddWithValue("@idDisciplina", (object?)idDisciplina ?? DBNull.Value);
        await command.ExecuteNonQueryAsync();
    }

    public async Task DeleteLivro(int idLivro)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"DELETE FROM BookBridge.dbo.Livro WHERE idLivro = @idLivro";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@idLivro", idLivro);
        await command.ExecuteNonQueryAsync();
    }
}
