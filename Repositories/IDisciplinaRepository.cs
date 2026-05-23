public interface IDisciplinaRepository
{
    Task<List<object>> GetDisciplinas();
    Task<object?> GetDisciplinaById(int idDisciplina);
    Task InsertDisciplina(string nome, string? curso);
    Task UpdateDisciplina(int idDisciplina, string nome, string? curso);
    Task DeleteDisciplina(int idDisciplina);
}
