using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class DisciplinaController : ControllerBase
{
    private readonly IDisciplinaRepository _disciplinaRepository;

    public DisciplinaController(IDisciplinaRepository disciplinaRepository)
    {
        _disciplinaRepository = disciplinaRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetDisciplinas()
    {
        var disciplinas = await _disciplinaRepository.GetDisciplinas();
        return Ok(disciplinas);
    }

    [HttpGet("{idDisciplina}")]
    public async Task<IActionResult> GetDisciplinaById(int idDisciplina)
    {
        var disciplina = await _disciplinaRepository.GetDisciplinaById(idDisciplina);
        if (disciplina is null)
            return NotFound("Disciplina não encontrada");
        return Ok(disciplina);
    }

    // curso é opcional
    [HttpPost]
    public async Task<IActionResult> InsertDisciplina(
        [FromQuery] string  nome,
        [FromQuery] string? curso = null)
    {
        await _disciplinaRepository.InsertDisciplina(nome, curso);
        return Ok("Disciplina criada");
    }

    [HttpPut("{idDisciplina}")]
    public async Task<IActionResult> UpdateDisciplina(
        int idDisciplina,
        [FromQuery] string  nome,
        [FromQuery] string? curso = null)
    {
        await _disciplinaRepository.UpdateDisciplina(idDisciplina, nome, curso);
        return Ok("Disciplina atualizada");
    }

    [HttpDelete("{idDisciplina}")]
    public async Task<IActionResult> DeleteDisciplina(int idDisciplina)
    {
        await _disciplinaRepository.DeleteDisciplina(idDisciplina);
        return Ok("Disciplina apagada");
    }
}
