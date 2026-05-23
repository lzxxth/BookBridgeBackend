public interface IMensagemRepository
{
    Task<List<object>> GetMensagens();
    Task<object?> GetMensagemById(int idMensagem);
    Task InsertMensagem(int idRemetente, int idDestinatario, int? idHistorico, string conteudo);
    Task UpdateMensagem(int idMensagem, bool lida);
    Task DeleteMensagem(int idMensagem);
}
