using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CategoriaController : ControllerBase
{
  private readonly ICategoriaRepository _categoriaRepository;  

  public CategoriaController(ICategoriaRepository categoriaRepository) {
    _categoriaRepository = categoriaRepository;
  }

  [HttpGet]
  public async Task<IActionResult> GetCategorias() {
    var categorias = await _categoriaRepository.GetCategorias();
    return Ok(categorias);
  }

  [HttpGet("{idCategoria}")]
  public async Task<IActionResult> GetCategoriaById(int idCategoria) {
    var categoria = await _categoriaRepository.GetCategoriaById(idCategoria);
    return Ok(categoria);
  }
 
  [HttpPost]
  public async Task<IActionResult> InsertCategoria (
      [FromQuery] string nome)
  {
    await _categoriaRepository.InsertCategoria(nome);
    return Ok("Categoria criada");
  }
  
  [HttpPut("{idCategoria}")]
  public async Task<IActionResult> UpdateCategoria (
      int idCategoria,
      [FromQuery] string nome)
  {
    await _categoriaRepository.UpdateCategoria(idCategoria, nome);
    return Ok("Categoria atualizada");
  }
  
  [HttpDelete("{idCategoria}")]
  public async Task<IActionResult> DeleteCategoria (
      int idCategoria)
  {
    await _categoriaRepository.DeleteCategoria(idCategoria);
    return Ok("Categoria apagada");
  }
}
