public interface IUserRepository
{
    Task<List<object>> GetUsers();
    Task<object?> GetUserById(int idUser);
    Task InsertUser(string nome, string email, string passwordHash, int idTipoUser, string? curso, string? instituicao, bool ativo);
    Task UpdateUser(int idUser, string nome, string email, string passwordHash, int idTipoUser, string? curso, string? instituicao, bool ativo);
    Task DeleteUser(int idUser);
}
