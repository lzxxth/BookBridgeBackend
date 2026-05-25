using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/[controller]")]
public class TipoAnuncioController : ControllerBase
{
  private readonly ITipoAnuncioRepository _tipoAnuncioRepository;
  public TipoAnuncioController(ITipoAnuncioRepository tipoAnuncioRepository) {
    _tipoAnuncioRepository = tipoAnuncioRepository;
  }
  [HttpGet]
  public async Task<IActionResult> GetTipoAnuncios() {
    var tipoAnuncios = await _tipoAnuncioRepository.GetTipoAnuncios();
    return Ok(tipoAnuncios);
  }
  [HttpGet("{idTipoAnuncio}")]
  public async Task<IActionResult> GetTipoAnuncioById(int idTipoAnuncio) {
    var tipoAnuncio = await _tipoAnuncioRepository.GetTipoAnuncioById(idTipoAnuncio);
    return Ok(tipoAnuncio);
  }
  [HttpPost]
  public async Task<IActionResult> InsertTipoAnuncio(
      [FromQuery] string descricao)
  {
    await _tipoAnuncioRepository.InsertTipoAnuncio(descricao);
    return Ok("TipoAnuncio criado");
  }
  [HttpPut("{idTipoAnuncio}")]
  public async Task<IActionResult> UpdateTipoAnuncio(
      int idTipoAnuncio,
      [FromQuery] string descricao)
  {
    await _tipoAnuncioRepository.UpdateTipoAnuncio(idTipoAnuncio, descricao);
    return Ok("TipoAnuncio atualizado");
  }
  [HttpDelete("{idTipoAnuncio}")]
  public async Task<IActionResult> DeleteTipoAnuncio(
      int idTipoAnuncio)
  {
    await _tipoAnuncioRepository.DeleteTipoAnuncio(idTipoAnuncio);
    return Ok("TipoAnuncio apagado");
  }
}
