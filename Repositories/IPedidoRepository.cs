public interface IPedidoRepository
{
    Task<List<object>> GetPedidos();
    Task<object?> GetPedidoById(int idPedido);

    Task InsertPedido(int idUser, int idLivro, string? descricao);

    Task UpdatePedido(int idPedido, int idUser, int idLivro, string? descricao, string estado);

    Task DeletePedido(int idPedido);
}