using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class LivroController : ControllerBase
{
  private readonly ILivroRepository _livroRepository;  

  public LivroController(ILivroRepository livroRepository) {
    _livroRepository = livroRepository;
  }

  [HttpGet]
  public async Task<IActionResult> GetLivros() {
    var livros = await _livroRepository.GetLivros();
    return Ok(livros);
  }

  [HttpGet("{idLivro}")]
  public async Task<IActionResult> GetLivroById(int idLivro) {
    var livro = await _livroRepository.GetLivroById(idLivro);
    return Ok(livro);
  }

  [HttpPost]
  public async Task<IActionResult> InsertLivro (
      [FromQuery] string titulo,
      [FromQuery] int idCategoria,
      [FromQuery] int idAutor)
  {
    await _livroRepository.InsertLivro(titulo, idCategoria, idAutor);
    return Ok("Livro criado");
  }

  [HttpPut("{idLivro}")]
  public async Task<IActionResult> UpdateLivro (
    int idLivro,
    [FromQuery] string titulo,
    [FromQuery] int idCategoria,
    [FromQuery] int idAutor)
  {
    await _livroRepository.UpdateLivro(idLivro, titulo, idCategoria, idAutor);
    return Ok("livro atualizado");
  }

  [HttpDelete("{idLivro}")]
  public async Task<IActionResult> DeleteLivro (
      int idLivro)
  {
    await _livroRepository.DeleteLivro(idLivro);
    return Ok("Livro apagado");
  } 
}

