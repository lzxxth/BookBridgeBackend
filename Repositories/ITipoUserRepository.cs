public interface ITipoUserRepository
{
    Task<List<object>> GetTipoUsers();
    Task<object?> GetTipoUserById(int idTipoUser);
    Task InsertTipoUser(string descricao);
    Task UpdateTipoUser(int idTipoUser, string descricao);
    Task DeleteTipoUser(int idTipoUser);
}
