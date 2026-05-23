using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AvaliacaoController : ControllerBase
{
    private readonly IAvaliacaoRepository _avaliacaoRepository;

    public AvaliacaoController(IAvaliacaoRepository avaliacaoRepository)
    {
        _avaliacaoRepository = avaliacaoRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAvaliacoes()
    {
        var avaliacoes = await _avaliacaoRepository.GetAvaliacoes();
        return Ok(avaliacoes);
    }

    [HttpGet("{idAvaliacao}")]
    public async Task<IActionResult> GetAvaliacaoById(int idAvaliacao)
    {
        var avaliacao = await _avaliacaoRepository.GetAvaliacaoById(idAvaliacao);
        if (avaliacao is null)
            return NotFound("Avaliação não encontrada");
        return Ok(avaliacao);
    }

    // rating aceita: 1 a 5
    // comentario é opcional
    // cada avaliador só pode avaliar uma vez por histórico (garantido pela BD)
    [HttpPost]
    public async Task<IActionResult> InsertAvaliacao(
        [FromQuery] int     idUserAvaliado,
        [FromQuery] int     idUserAvaliador,
        [FromQuery] int     idHistorico,
        [FromQuery] int     rating,
        [FromQuery] string? comentario = null)
    {
        await _avaliacaoRepository.InsertAvaliacao(idUserAvaliado, idUserAvaliador, idHistorico, rating, comentario);
        return Ok("Avaliação criada");
    }

    // PUT apenas permite editar rating e comentario
    [HttpPut("{idAvaliacao}")]
    public async Task<IActionResult> UpdateAvaliacao(
        int idAvaliacao,
        [FromQuery] int     rating,
        [FromQuery] string? comentario = null)
    {
        await _avaliacaoRepository.UpdateAvaliacao(idAvaliacao, rating, comentario);
        return Ok("Avaliação atualizada");
    }

    [HttpDelete("{idAvaliacao}")]
    public async Task<IActionResult> DeleteAvaliacao(int idAvaliacao)
    {
        await _avaliacaoRepository.DeleteAvaliacao(idAvaliacao);
        return Ok("Avaliação apagada");
    }
}
