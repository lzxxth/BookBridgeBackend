using Microsoft.Data.SqlClient;

public class PedidoRepository : IPedidoRepository
{
    private readonly string _connectionString;

    public PedidoRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    }

    public async Task<List<object>> GetPedidos()
    {
        var results = new List<object>();

        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var query = @"SELECT * FROM BookBridge.dbo.Pedido";

        using var command = new SqlCommand(query, connection);
        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            results.Add(new
            {
                idPedido = reader.GetInt32(0),
                idUser = reader.GetInt32(1),
                idLivro = reader.GetInt32(2),
                descricao = reader.IsDBNull(3) ? null : reader.GetString(3),
                estado = reader.GetString(4),
                dataRegisto = reader.GetDateTime(5)
            });
        }

        return results;
    }

    public async Task<object?> GetPedidoById(int idPedido)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var query = @"SELECT * 
                      FROM BookBridge.dbo.Pedido 
                      WHERE idPedido = @idPedido";

        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@idPedido", idPedido);

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            return new
            {
                idPedido = reader.GetInt32(0),
                idUser = reader.GetInt32(1),
                idLivro = reader.GetInt32(2),
                descricao = reader.IsDBNull(3) ? null : reader.GetString(3),
                estado = reader.GetString(4),
                dataRegisto = reader.GetDateTime(5)
            };
        }

        return null;
    }

    public async Task InsertPedido(int idUser, int idLivro, string? descricao)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var query = @"INSERT INTO BookBridge.dbo.Pedido
                      (idUser, idLivro, descricao)
                      VALUES
                      (@idUser, @idLivro, @descricao)";

        using var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@idUser", idUser);
        command.Parameters.AddWithValue("@idLivro", idLivro);
        command.Parameters.AddWithValue("@descricao", (object?)descricao ?? DBNull.Value);

        await command.ExecuteNonQueryAsync();
    }

    public async Task UpdatePedido(int idPedido, int idUser, int idLivro, string? descricao, string estado)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var query = @"UPDATE BookBridge.dbo.Pedido
                      SET idUser = @idUser,
                          idLivro = @idLivro,
                          descricao = @descricao,
                          estado = @estado
                      WHERE idPedido = @idPedido";

        using var command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@idPedido", idPedido);
        command.Parameters.AddWithValue("@idUser", idUser);
        command.Parameters.AddWithValue("@idLivro", idLivro);
        command.Parameters.AddWithValue("@descricao", (object?)descricao ?? DBNull.Value);
        command.Parameters.AddWithValue("@estado", estado);

        await command.ExecuteNonQueryAsync();
    }

    public async Task DeletePedido(int idPedido)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var query = @"DELETE FROM BookBridge.dbo.Pedido 
                      WHERE idPedido = @idPedido";

        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@idPedido", idPedido);

        await command.ExecuteNonQueryAsync();
    }
}