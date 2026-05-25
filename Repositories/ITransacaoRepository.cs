public interface ITransacaoRepository
{
  Task<List<object>> GetTransacoes();
  Task<object?> GetTransacaoById(int idTransacao);
  Task<List<object>> GetTransacoesByAnuncio(int idAnuncio);
  Task<List<object>> GetTransacoesByComprador(int idUserComprador);
  Task InsertTransacao(int idAnuncio, int idUserComprador, DateTime? dataFimPrevista, string? observacoes);
  Task UpdateTransacao(int idTransacao, string estado, DateTime? dataFimPrevista, DateTime? dataFimEfetiva, string? observacoes);
  Task DeleteTransacao(int idTransacao);
  Task<int> GetDiasTransacao(int idTransacao);
  Task<List<object>> GetTransacoesAtivas();
  Task<object?> GetIndicadores();
  Task<int> IniciarTransacao(
    int idAnuncio,
    int idUserComprador,
    DateTime? dataFimPrevista);
  Task AtualizarTransacao(
    int idTransacao,
    string novoEstado,
    DateTime? dataFimEfetiva);
}
