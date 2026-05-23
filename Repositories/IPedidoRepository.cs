public interface IPedidoRepository
{
    Task<List<object>> GetPedidos();
    Task<object?> GetPedidoById(int idPedido);
    Task InsertPedido(int idHistorico, DateOnly? dataInicio, DateOnly? dataFimPrevista);
    Task UpdatePedido(int idPedido, int idHistorico, DateOnly? dataInicio, DateOnly? dataFimPrevista, DateOnly? dataFimEfetiva);
    Task DeletePedido(int idPedido);
}
