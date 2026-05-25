using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/[controller]")]
public class LivroCategoriaController : ControllerBase
{
  private readonly ILivroCategoriaRepository _livroCategoriaRepository;
  public LivroCategoriaController(ILivroCategoriaRepository livroCategoriaRepository) {
    _livroCategoriaRepository = livroCategoriaRepository;
  }
  [HttpGet]
  public async Task<IActionResult> GetLivroCategorias() {
    var livroCategorias = await _livroCategoriaRepository.GetLivroCategorias();
    return Ok(livroCategorias);
  }
  [HttpGet("livro/{idLivro}")]
  public async Task<IActionResult> GetCategoriasByLivro(int idLivro) {
    var categorias = await _livroCategoriaRepository.GetCategoriasByLivro(idLivro);
    return Ok(categorias);
  }
  [HttpGet("categoria/{idCategoria}")]
  public async Task<IActionResult> GetLivrosByCategoria(int idCategoria) {
    var livros = await _livroCategoriaRepository.GetLivrosByCategoria(idCategoria);
    return Ok(livros);
  }
  [HttpPost]
  public async Task<IActionResult> InsertLivroCategoria(
      [FromQuery] int idLivro,
      [FromQuery] int idCategoria)
  {
    await _livroCategoriaRepository.InsertLivroCategoria(idLivro, idCategoria);
    return Ok("LivroCategoria criado");
  }
  [HttpDelete("{idLivro}/{idCategoria}")]
  public async Task<IActionResult> DeleteLivroCategoria(
      int idLivro,
      int idCategoria)
  {
    await _livroCategoriaRepository.DeleteLivroCategoria(idLivro, idCategoria);
    return Ok("LivroCategoria apagado");
  }
}