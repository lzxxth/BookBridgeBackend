using Microsoft.Data.SqlClient;

public class PedidoRepository : IPedidoRepository
{
    private readonly string _connectionString;

    public PedidoRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
          ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    }

    private static object DateOnlyToParam(DateOnly? d) =>
        d.HasValue ? (object)d.Value.ToDateTime(TimeOnly.MinValue) : DBNull.Value;

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
                idPedido        = reader.GetInt32(0),
                idHistorico     = reader.GetInt32(1),
                dataInicio      = reader.IsDBNull(2) ? (DateOnly?)null : DateOnly.FromDateTime(reader.GetDateTime(2)),
                dataFimPrevista = reader.IsDBNull(3) ? (DateOnly?)null : DateOnly.FromDateTime(reader.GetDateTime(3)),
                dataFimEfetiva  = reader.IsDBNull(4) ? (DateOnly?)null : DateOnly.FromDateTime(reader.GetDateTime(4))
            });
        }
        return results;
    }

    public async Task<object?> GetPedidoById(int idPedido)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"SELECT * FROM BookBridge.dbo.Pedido WHERE idPedido = @idPedido";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@idPedido", idPedido);
        using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new
            {
                idPedido        = reader.GetInt32(0),
                idHistorico     = reader.GetInt32(1),
                dataInicio      = reader.IsDBNull(2) ? (DateOnly?)null : DateOnly.FromDateTime(reader.GetDateTime(2)),
                dataFimPrevista = reader.IsDBNull(3) ? (DateOnly?)null : DateOnly.FromDateTime(reader.GetDateTime(3)),
                dataFimEfetiva  = reader.IsDBNull(4) ? (DateOnly?)null : DateOnly.FromDateTime(reader.GetDateTime(4))
            };
        }
        return null;
    }

    // dataInicio e dataFimPrevista são opcionais no INSERT
    // a BD valida: dataFimPrevista >= dataInicio (CHECK constraint)
    public async Task InsertPedido(int idHistorico, DateOnly? dataInicio, DateOnly? dataFimPrevista)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"INSERT INTO BookBridge.dbo.Pedido
                          (idHistorico, dataInicio, dataFimPrevista)
                      VALUES
                          (@idHistorico, @dataInicio, @dataFimPrevista)";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@idHistorico",     idHistorico);
        command.Parameters.AddWithValue("@dataInicio",      DateOnlyToParam(dataInicio));
        command.Parameters.AddWithValue("@dataFimPrevista", DateOnlyToParam(dataFimPrevista));
        await command.ExecuteNonQueryAsync();
    }

    public async Task UpdatePedido(int idPedido, int idHistorico, DateOnly? dataInicio, DateOnly? dataFimPrevista, DateOnly? dataFimEfetiva)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"UPDATE BookBridge.dbo.Pedido
                      SET idHistorico     = @idHistorico,
                          dataInicio      = @dataInicio,
                          dataFimPrevista = @dataFimPrevista,
                          dataFimEfetiva  = @dataFimEfetiva
                      WHERE idPedido = @idPedido";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@idPedido",        idPedido);
        command.Parameters.AddWithValue("@idHistorico",     idHistorico);
        command.Parameters.AddWithValue("@dataInicio",      DateOnlyToParam(dataInicio));
        command.Parameters.AddWithValue("@dataFimPrevista", DateOnlyToParam(dataFimPrevista));
        command.Parameters.AddWithValue("@dataFimEfetiva",  DateOnlyToParam(dataFimEfetiva));
        await command.ExecuteNonQueryAsync();
    }

    public async Task DeletePedido(int idPedido)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = @"DELETE FROM BookBridge.dbo.Pedido WHERE idPedido = @idPedido";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@idPedido", idPedido);
        await command.ExecuteNonQueryAsync();
    }
}
