using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class LivroAutorController : ControllerBase
{
    private readonly ILivroAutorRepository _livroAutorRepository;

    public LivroAutorController(ILivroAutorRepository livroAutorRepository)
    {
        _livroAutorRepository = livroAutorRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetLivroAutores()
    {
        var livroAutores = await _livroAutorRepository.GetLivroAutores();
        return Ok(livroAutores);
    }

    // GET api/LivroAutor/livro/3  → autores do livro 3
    [HttpGet("livro/{idLivro}")]
    public async Task<IActionResult> GetAutoresByLivro(int idLivro)
    {
        var autores = await _livroAutorRepository.GetAutoresByLivro(idLivro);
        return Ok(autores);
    }

    // GET api/LivroAutor/autor/5  → livros do autor 5
    [HttpGet("autor/{idAutor}")]
    public async Task<IActionResult> GetLivrosByAutor(int idAutor)
    {
        var livros = await _livroAutorRepository.GetLivrosByAutor(idAutor);
        return Ok(livros);
    }

    [HttpPost]
    public async Task<IActionResult> InsertLivroAutor(
        [FromQuery] int idLivro,
        [FromQuery] int idAutor,
        [FromQuery] int ordemAutor = 1)
    {
        await _livroAutorRepository.InsertLivroAutor(idLivro, idAutor, ordemAutor);
        return Ok("Relação Livro-Autor criada");
    }

    // PUT atualiza apenas ordemAutor (a PK composta não muda)
    [HttpPut]
    public async Task<IActionResult> UpdateLivroAutor(
        [FromQuery] int idLivro,
        [FromQuery] int idAutor,
        [FromQuery] int ordemAutor)
    {
        await _livroAutorRepository.UpdateLivroAutor(idLivro, idAutor, ordemAutor);
        return Ok("Relação Livro-Autor atualizada");
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteLivroAutor(
        [FromQuery] int idLivro,
        [FromQuery] int idAutor)
    {
        await _livroAutorRepository.DeleteLivroAutor(idLivro, idAutor);
        return Ok("Relação Livro-Autor apagada");
    }
}
