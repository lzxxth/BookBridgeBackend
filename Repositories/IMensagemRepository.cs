public interface IMensagemRepository
{
    Task<List<object>> GetMensagens();

    Task<object?> GetMensagemById(int idMensagem);

    Task InsertMensagem(
        int idRemetente,
        int idDestinatario,
        int? idAnuncio,
        string conteudo);

    Task UpdateMensagem(
        int idMensagem,
        int idRemetente,
        int idDestinatario,
        int? idAnuncio,
        string conteudo,
        bool lida);

    Task DeleteMensagem(int idMensagem);
}