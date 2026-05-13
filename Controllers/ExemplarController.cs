using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ExemplarController : ControllerBase
{
  private readonly IExemplarRepository _exemplarRepository;  

  public ExemplarController(IExemplarRepository exemplarRepository) {
    _exemplarRepository = exemplarRepository;
  }

  [HttpGet]
  public async Task<IActionResult> GetExemplares() {
    var exemplares = await _exemplarRepository.GetExemplares();
    return Ok(exemplares);
  }

  [HttpGet("{idExemplar}")]
  public async Task<IActionResult> GetExemplarById(int idExemplar) {
    var exemplar = await _exemplarRepository.GetExemplarById(idExemplar);
    return Ok(exemplar);
  }

  [HttpPost]
  public async Task<IActionResult> InsertExemplar(
    [FromQuery] int idLivro,
    [FromQuery] int idProprietario,
    [FromQuery] string estado,
    [FromQuery] bool disponivel,
    [FromQuery] DateTime dataAdicionado)
  {
    await _exemplarRepository.InsertExemplar(idLivro, idProprietario,estado,disponivel,dataAdicionado);
    return Ok("Exemplar adicionado");
  }

  [HttpPut("{idExemplar}")]
  public async Task<IActionResult> UpdateExemplar(
      int idExemplar,
      [FromQuery] int idLivro,
      [FromQuery] int idProprietario,
      [FromQuery] string estado,
      [FromQuery] bool disponivel,
      [FromQuery] DateTime dataAdicionado)
      {
        await _exemplarRepository.UpdateExemplar(idExemplar,idLivro,idProprietario,estado,disponivel,dataAdicionado);
        return Ok("Exemplar atualizado");
      }
  [HttpDelete("{idExemplar}")]
  public async Task<IActionResult> DeleteExemplar(int idExemplar) {
    await _exemplarRepository.DeleteExemplar(idExemplar);
    return Ok("Exemplar removido");
  }
}
