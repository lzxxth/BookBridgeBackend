using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ExemplarController : ControllerBase
{
    private readonly IExemplarRepository _exemplarRepository;

    public ExemplarController(IExemplarRepository exemplarRepository)
    {
        _exemplarRepository = exemplarRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetExemplares()
    {
        var exemplares = await _exemplarRepository.GetExemplares();
        return Ok(exemplares);
    }

    [HttpGet("{idExemplar}")]
    public async Task<IActionResult> GetExemplarById(int idExemplar)
    {
        var exemplar = await _exemplarRepository.GetExemplarById(idExemplar);
        if (exemplar is null)
            return NotFound("Exemplar não encontrado");
        return Ok(exemplar);
    }

    // estado aceita: 'novo', 'bom', 'usado', 'danificado'
    // descricao é opcional
    [HttpPost]
    public async Task<IActionResult> InsertExemplar(
        [FromQuery] int     idLivro,
        [FromQuery] int     idProprietario,
        [FromQuery] string  estado,
        [FromQuery] bool    disponivel = true,
        [FromQuery] string? descricao  = null)
    {
        await _exemplarRepository.InsertExemplar(idLivro, idProprietario, estado, disponivel, descricao);
        return Ok("Exemplar criado");
    }

    [HttpPut("{idExemplar}")]
    public async Task<IActionResult> UpdateExemplar(
        int idExemplar,
        [FromQuery] int     idLivro,
        [FromQuery] int     idProprietario,
        [FromQuery] string  estado,
        [FromQuery] bool    disponivel,
        [FromQuery] string? descricao = null)
    {
        await _exemplarRepository.UpdateExemplar(idExemplar, idLivro, idProprietario, estado, disponivel, descricao);
        return Ok("Exemplar atualizado");
    }

    [HttpDelete("{idExemplar}")]
    public async Task<IActionResult> DeleteExemplar(int idExemplar)
    {
        await _exemplarRepository.DeleteExemplar(idExemplar);
        return Ok("Exemplar apagado");
    }
}
