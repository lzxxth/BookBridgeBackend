public interface IAlertaRepository
{
    Task<List<object>> GetAlertas();

    Task<object?> GetAlertaById(int idAlerta);

    Task InsertAlerta(
        int idUser,
        int? idAnuncio,
        int? idPedido,
        string mensagem);

    Task UpdateAlerta(
        int idAlerta,
        int idUser,
        int? idAnuncio,
        int? idPedido,
        string mensagem,
        bool lido);

    Task DeleteAlerta(int idAlerta);
}