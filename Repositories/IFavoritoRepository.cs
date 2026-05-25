public interface IFavoritoRepository
{
    Task<List<object>> GetFavoritos();

    Task<object?> GetFavoritoById(int idFavorito);

    Task InsertFavorito(int idUser, int idAnuncio);

    Task UpdateFavorito(
        int idFavorito,
        int idUser,
        int idAnuncio);

    Task DeleteFavorito(int idFavorito);
}