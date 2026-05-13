using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AutorController : ControllerBase
{
  private readonly IAutorRepository _autorRepository;  

  public AutorController(IAutorRepository autorRepository) {
    _autorRepository = autorRepository;
  }

  [HttpGet]
  public async Task<IActionResult> GetAutores() {
    var autores = await _autorRepository.GetAutores();
    return Ok(autores);
  }

  [HttpGet("{idAutor}")]
  public async Task<IActionResult> GetAutorById(int idAutor) {
    var autor = await _autorRepository.GetAutorById(idAutor);
    return Ok(autor);
  }

  [HttpPost]
  public async Task<IActionResult> InsertAutor (
      [FromQuery] string nomeAutor)
  {
    await _autorRepository.InsertAutor(nomeAutor);
    return Ok("Autor criado");
  }
  
  [HttpPut("{idAutor}")]
  public async Task<IActionResult> UpdateAutor (
      int idAutor,
      [FromQuery] string nomeAutor)
  {
    await _autorRepository.UpdateAutor(idAutor, nomeAutor);
    return Ok("Autor atualizado");
  }
  
  [HttpDelete("{idAutor}")]
  public async Task<IActionResult> DeleteAutor (
      int idAutor)
  {
    await _autorRepository.DeleteAutor(idAutor);
    return Ok("Autor apagado");
  }
}
