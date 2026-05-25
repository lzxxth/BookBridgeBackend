using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class HistoricoController : ControllerBase
{
    private readonly IHistoricoRepository _historicoRepository;

    public HistoricoController(IHistoricoRepository historicoRepository)
    {
        _historicoRepository = historicoRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetHistoricos()
    {
        var historicos = await _historicoRepository.GetHistoricos();
        return Ok(historicos);
    }

    [HttpGet("{idHistorico}")]
    public async Task<IActionResult> GetHistoricoById(int idHistorico)
    {
        var historico = await _historicoRepository.GetHistoricoById(idHistorico);
        return Ok(historico);
    }

    [HttpPost]
    public async Task<IActionResult> InsertHistorico(
        [FromQuery] int idTransacao,
        [FromQuery] int idUser,
        [FromQuery] string? descricao)
    {
        await _historicoRepository.InsertHistorico(idTransacao, idUser, descricao);
        return Ok("Historico criado");
    }

    [HttpPut("{idHistorico}")]
    public async Task<IActionResult> UpdateHistorico(
        int idHistorico,
        [FromQuery] int idTransacao,
        [FromQuery] int idUser,
        [FromQuery] string? descricao)
    {
        await _historicoRepository.UpdateHistorico(
            idHistorico,
            idTransacao,
            idUser,
            descricao);

        return Ok("Historico atualizado");
    }

    [HttpDelete("{idHistorico}")]
    public async Task<IActionResult> DeleteHistorico(int idHistorico)
    {
        await _historicoRepository.DeleteHistorico(idHistorico);
        return Ok("Historico apagado");
    }

    [HttpGet("detalhado")]
    public async Task<IActionResult> GetHistoricoDetalhado()
    {
        var result = await _historicoRepository.GetHistoricoDetalhado();
        return Ok(result);
    }
}