using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/[controller]")]
public class AnuncioController : ControllerBase
{
  private readonly IAnuncioRepository _anuncioRepository;
  public AnuncioController(IAnuncioRepository anuncioRepository) {
    _anuncioRepository = anuncioRepository;
  }
  [HttpGet]
  public async Task<IActionResult> GetAnuncios() {
    var anuncios = await _anuncioRepository.GetAnuncios();
    return Ok(anuncios);
  }
  [HttpGet("{idAnuncio}")]
  public async Task<IActionResult> GetAnuncioById(int idAnuncio) {
    var anuncio = await _anuncioRepository.GetAnuncioById(idAnuncio);
    return Ok(anuncio);
  }
  [HttpGet("user/{idUser}")]
  public async Task<IActionResult> GetAnunciosByUser(int idUser) {
    var anuncios = await _anuncioRepository.GetAnunciosByUser(idUser);
    return Ok(anuncios);
  }
  [HttpGet("tipo/{idTipoAnuncio}")]
  public async Task<IActionResult> GetAnunciosByTipo(int idTipoAnuncio) {
    var anuncios = await _anuncioRepository.GetAnunciosByTipo(idTipoAnuncio);
    return Ok(anuncios);
  }

  [HttpGet("disponivel/{idAnuncio}")]
  public async Task<IActionResult> AnuncioDisponivel(int idAnuncio)
  {
      var disponivel = await _anuncioRepository.AnuncioDisponivel(idAnuncio);
      return Ok(disponivel);
  }

  [HttpGet("ativos")]
  public async Task<IActionResult> GetAnunciosAtivos()
  {
      var result = await _anuncioRepository.GetAnunciosAtivos();
      return Ok(result);
  }

  [HttpPost]
  public async Task<IActionResult> InsertAnuncio(
      [FromQuery] int      idExemplar,
      [FromQuery] int      idUser,
      [FromQuery] int      idTipoAnuncio,
      [FromQuery] string   titulo,
      [FromQuery] string?  descricao,
      [FromQuery] decimal? preco)
  {
    await _anuncioRepository.InsertAnuncio(idExemplar, idUser, idTipoAnuncio, titulo, descricao, preco);
    return Ok("Anuncio criado");
  }
  [HttpPut("{idAnuncio}")]
  public async Task<IActionResult> UpdateAnuncio(
      int              idAnuncio,
      [FromQuery] int      idTipoAnuncio,
      [FromQuery] string   titulo,
      [FromQuery] string?  descricao,
      [FromQuery] decimal? preco,
      [FromQuery] string   estado)
  {
    await _anuncioRepository.UpdateAnuncio(idAnuncio, idTipoAnuncio, titulo, descricao, preco, estado);
    return Ok("Anuncio atualizado");
  }
  [HttpDelete("{idAnuncio}")]
  public async Task<IActionResult> DeleteAnuncio(int idAnuncio) {
    await _anuncioRepository.DeleteAnuncio(idAnuncio);
    return Ok("Anuncio apagado");
  }

  [HttpPost("publicar")]
  public async Task<IActionResult> PublicarAnuncio(
      [FromQuery] int idExemplar,
      [FromQuery] int idUser,
      [FromQuery] int idTipoAnuncio,
      [FromQuery] string titulo,
      [FromQuery] string? descricao,
      [FromQuery] decimal? preco)
  {
      var id = await _anuncioRepository.PublicarAnuncio(
          idExemplar,
          idUser,
          idTipoAnuncio,
          titulo,
          descricao,
          preco
      );

      return Ok(new { idAnuncio = id });
  }

  [HttpGet("pesquisar")]
  public async Task<IActionResult> PesquisarAnuncios(
      [FromQuery] string? titulo,
      [FromQuery] string? nomeAutor,
      [FromQuery] int? idCategoria,
      [FromQuery] int? idDisciplina,
      [FromQuery] int? idTipoAnuncio)
  {
      var result = await _anuncioRepository.PesquisarAnuncios(
          titulo,
          nomeAutor,
          idCategoria,
          idDisciplina,
          idTipoAnuncio);

      return Ok(result);
  }
}
