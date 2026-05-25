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
        return Ok(avaliacao);
    }

    [HttpPost]
    public async Task<IActionResult> InsertAvaliacao(
        [FromQuery] int idUserAvaliado,
        [FromQuery] int idUserAvaliador,
        [FromQuery] int idTransacao,
        [FromQuery] int rating,
        [FromQuery] string? comentario)
    {
        await _avaliacaoRepository.InsertAvaliacao(
            idUserAvaliado,
            idUserAvaliador,
            idTransacao,
            rating,
            comentario);

        return Ok("Avaliacao criada");
    }

    [HttpPut("{idAvaliacao}")]
    public async Task<IActionResult> UpdateAvaliacao(
        int idAvaliacao,
        [FromQuery] int idUserAvaliado,
        [FromQuery] int idUserAvaliador,
        [FromQuery] int idTransacao,
        [FromQuery] int rating,
        [FromQuery] string? comentario)
    {
        await _avaliacaoRepository.UpdateAvaliacao(
            idAvaliacao,
            idUserAvaliado,
            idUserAvaliador,
            idTransacao,
            rating,
            comentario);

        return Ok("Avaliacao atualizada");
    }

    [HttpDelete("{idAvaliacao}")]
    public async Task<IActionResult> DeleteAvaliacao(int idAvaliacao)
    {
        await _avaliacaoRepository.DeleteAvaliacao(idAvaliacao);
        return Ok("Avaliacao apagada");
    }

    [HttpGet("media/{idUser}")]
    public async Task<IActionResult> GetMediaAvaliacao(int idUser)
    {
        var media = await _avaliacaoRepository.GetMediaAvaliacao(idUser);
        return Ok(media);
    }

    [HttpGet("detalhadas")]
    public async Task<IActionResult> GetAvaliacoesDetalhadas()
    {
        var result = await _avaliacaoRepository.GetAvaliacoesDetalhadas();
        return Ok(result);
    }
}