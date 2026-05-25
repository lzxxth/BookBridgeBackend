using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class FavoritoController : ControllerBase
{
    private readonly IFavoritoRepository _favoritoRepository;

    public FavoritoController(IFavoritoRepository favoritoRepository)
    {
        _favoritoRepository = favoritoRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetFavoritos()
    {
        var favoritos = await _favoritoRepository.GetFavoritos();
        return Ok(favoritos);
    }

    [HttpGet("{idFavorito}")]
    public async Task<IActionResult> GetFavoritoById(int idFavorito)
    {
        var favorito = await _favoritoRepository.GetFavoritoById(idFavorito);
        return Ok(favorito);
    }

    [HttpPost]
    public async Task<IActionResult> InsertFavorito(
        [FromQuery] int idUser,
        [FromQuery] int idAnuncio)
    {
        await _favoritoRepository.InsertFavorito(idUser, idAnuncio);
        return Ok("Favorito criado");
    }

    [HttpPut("{idFavorito}")]
    public async Task<IActionResult> UpdateFavorito(
        int idFavorito,
        [FromQuery] int idUser,
        [FromQuery] int idAnuncio)
    {
        await _favoritoRepository.UpdateFavorito(
            idFavorito,
            idUser,
            idAnuncio);

        return Ok("Favorito atualizado");
    }

    [HttpDelete("{idFavorito}")]
    public async Task<IActionResult> DeleteFavorito(int idFavorito)
    {
        await _favoritoRepository.DeleteFavorito(idFavorito);
        return Ok("Favorito apagado");
    }
}