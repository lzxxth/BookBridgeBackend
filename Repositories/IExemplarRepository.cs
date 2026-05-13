public interface IExemplarRepository
{
  Task<List<object>> GetExemplares(); 
  Task<object?> GetExemplarById(int idExemplar);
  Task InsertExemplar(int idLivro, int idProprietario, string estado, bool disponivel, DateTime dataAdicionado);
  Task UpdateExemplar(int idExemplar, int idLivro, int idProprietario, string estado, bool disponivel, DateTime dataAdicionado); 
  Task DeleteExemplar(int idLivro);
}
