public interface ITipoAnuncioRepository
{
  Task<List<object>> GetTipoAnuncios();
  Task<object?> GetTipoAnuncioById(int idTipoAnuncio);
  Task InsertTipoAnuncio(string descricao);
  Task UpdateTipoAnuncio(int idTipoAnuncio, string descricao);
  Task DeleteTipoAnuncio(int idTipoAnuncio);
}
