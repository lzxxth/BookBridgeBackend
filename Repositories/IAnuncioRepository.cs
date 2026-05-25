public interface IAnuncioRepository
{
  Task<List<object>> GetAnuncios();
  Task<object?> GetAnuncioById(int idAnuncio);
  Task<List<object>> GetAnunciosByUser(int idUser);
  Task<List<object>> GetAnunciosByTipo(int idTipoAnuncio);
  Task InsertAnuncio(int idExemplar, int idUser, int idTipoAnuncio, string titulo, string? descricao, decimal? preco);
  Task UpdateAnuncio(int idAnuncio, int idTipoAnuncio, string titulo, string? descricao, decimal? preco, string estado);
  Task DeleteAnuncio(int idAnuncio);
  Task<bool> AnuncioDisponivel(int idAnuncio);

  Task<List<object>> GetAnunciosAtivos();

  Task<int> PublicarAnuncio(
    int idExemplar,
    int idUser,
    int idTipoAnuncio,
    string titulo,
    string? descricao,
    decimal? preco);

  Task<List<object>> PesquisarAnuncios(
    string? titulo,
    string? nomeAutor,
    int? idCategoria,
    int? idDisciplina,
    int? idTipoAnuncio);
}
