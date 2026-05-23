using Microsoft.Data.SqlClient;

public class LivroAutorRepository : ILivroAutorRepository
{
    private readonly string _connectionString;

    public LivroAutorRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
          ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    }

    public async Task<List<object>> GetLivroAutores()
    {
        var results = new List<object>();
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"SELECT * FROM BookBridge.dbo.LivroAutor ORDER BY idLivro, ordemAutor";
        using var command = new SqlCommand(query, connection);
        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            results.Add(new
            {
                idLivro    = reader.GetInt32(0),
                idAutor    = reader.GetInt32(1),
                ordemAutor = reader.GetInt32(2)
            });
        }
        return results;
    }

    public async Task<List<object>> GetAutoresByLivro(int idLivro)
    {
        var results = new List<object>();
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"SELECT * FROM BookBridge.dbo.LivroAutor
                      WHERE idLivro = @idLivro
                      ORDER BY ordemAutor";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@idLivro", idLivro);
        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            results.Add(new
            {
                idLivro    = reader.GetInt32(0),
                idAutor    = reader.GetInt32(1),
                ordemAutor = reader.GetInt32(2)
            });
        }
        return results;
    }

    public async Task<List<object>> GetLivrosByAutor(int idAutor)
    {
        var results = new List<object>();
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"SELECT * FROM BookBridge.dbo.LivroAutor
                      WHERE idAutor = @idAutor
                      ORDER BY idLivro";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@idAutor", idAutor);
        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            results.Add(new
            {
                idLivro    = reader.GetInt32(0),
                idAutor    = reader.GetInt32(1),
                ordemAutor = reader.GetInt32(2)
            });
        }
        return results;
    }

    // ordemAutor tem DEFAULT 1, mas é passado explicitamente para permitir ordenação correta
    public async Task InsertLivroAutor(int idLivro, int idAutor, int ordemAutor)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"INSERT INTO BookBridge.dbo.LivroAutor (idLivro, idAutor, ordemAutor)
                      VALUES (@idLivro, @idAutor, @ordemAutor)";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@idLivro",    idLivro);
        command.Parameters.AddWithValue("@idAutor",    idAutor);
        command.Parameters.AddWithValue("@ordemAutor", ordemAutor);
        await command.ExecuteNonQueryAsync();
    }

    public async Task UpdateLivroAutor(int idLivro, int idAutor, int ordemAutor)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"UPDATE BookBridge.dbo.LivroAutor
                      SET ordemAutor = @ordemAutor
                      WHERE idLivro = @idLivro AND idAutor = @idAutor";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@idLivro",    idLivro);
        command.Parameters.AddWithValue("@idAutor",    idAutor);
        command.Parameters.AddWithValue("@ordemAutor", ordemAutor);
        await command.ExecuteNonQueryAsync();
    }

    public async Task DeleteLivroAutor(int idLivro, int idAutor)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"DELETE FROM BookBridge.dbo.LivroAutor
                      WHERE idLivro = @idLivro AND idAutor = @idAutor";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@idLivro", idLivro);
        command.Parameters.AddWithValue("@idAutor", idAutor);
        await command.ExecuteNonQueryAsync();
    }
}
