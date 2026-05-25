using Microsoft.Data.SqlClient;

public class FavoritoRepository : IFavoritoRepository
{
    private readonly string _connectionString;

    public FavoritoRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException(
                "Connection string 'DefaultConnection' not found.");
    }

    public async Task<List<object>> GetFavoritos()
    {
        var results = new List<object>();

        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var query = @"SELECT * FROM BookBridge.dbo.Favorito";

        using var command = new SqlCommand(query, connection);
        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            results.Add(new
            {
                idFavorito = reader.GetInt32(0),
                idUser = reader.GetInt32(1),
                idAnuncio = reader.GetInt32(2),
                dataRegisto = reader.GetDateTime(3)
            });
        }

        return results;
    }

    public async Task<object?> GetFavoritoById(int idFavorito)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var query = @"SELECT * 
                      FROM BookBridge.dbo.Favorito
                      WHERE idFavorito = @idFavorito";

        using var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@idFavorito", idFavorito);

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            return new
            {
                idFavorito = reader.GetInt32(0),
                idUser = reader.GetInt32(1),
                idAnuncio = reader.GetInt32(2),
                dataRegisto = reader.GetDateTime(3)
            };
        }

        return null;
    }

    public async Task InsertFavorito(int idUser, int idAnuncio)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var query = @"INSERT INTO BookBridge.dbo.Favorito
                      (idUser, idAnuncio)
                      VALUES
                      (@idUser, @idAnuncio)";

        using var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@idUser", idUser);
        command.Parameters.AddWithValue("@idAnuncio", idAnuncio);

        await command.ExecuteNonQueryAsync();
    }

    public async Task UpdateFavorito(
        int idFavorito,
        int idUser,
        int idAnuncio)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var query = @"UPDATE BookBridge.dbo.Favorito
                      SET idUser = @idUser,
                          idAnuncio = @idAnuncio
                      WHERE idFavorito = @idFavorito";

        using var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@idFavorito", idFavorito);
        command.Parameters.AddWithValue("@idUser", idUser);
        command.Parameters.AddWithValue("@idAnuncio", idAnuncio);

        await command.ExecuteNonQueryAsync();
    }

    public async Task DeleteFavorito(int idFavorito)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var query = @"DELETE FROM BookBridge.dbo.Favorito
                      WHERE idFavorito = @idFavorito";

        using var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@idFavorito", idFavorito);

        await command.ExecuteNonQueryAsync();
    }
}