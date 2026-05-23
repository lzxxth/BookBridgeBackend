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
        if (historico is null)
            return NotFound("Histórico não encontrado");
        return Ok(historico);
    }

    // estado aceita: 'pendente', 'ativa', 'concluida', 'cancelada', 'recusada'
    // observacoes é opcional
    [HttpPost]
    public async Task<IActionResult> InsertHistorico(
        [FromQuery] int     idTipoTransacao,
        [FromQuery] int     idUserSolicitante,
        [FromQuery] int     idUserProprietario,
        [FromQuery] int     idExemplar,
        [FromQuery] string  estado,
        [FromQuery] string? observacoes = null)
    {
        await _historicoRepository.InsertHistorico(idTipoTransacao, idUserSolicitante, idUserProprietario, idExemplar, estado, observacoes);
        return Ok("Histórico criado");
    }

    // dataConclusao formato: yyyy-MM-dd
    [HttpPut("{idHistorico}")]
    public async Task<IActionResult> UpdateHistorico(
        int idHistorico,
        [FromQuery] int       idTipoTransacao,
        [FromQuery] int       idUserSolicitante,
        [FromQuery] int       idUserProprietario,
        [FromQuery] int       idExemplar,
        [FromQuery] string    estado,
        [FromQuery] DateOnly? dataConclusao = null,
        [FromQuery] string?   observacoes   = null)
    {
        await _historicoRepository.UpdateHistorico(idHistorico, idTipoTransacao, idUserSolicitante, idUserProprietario, idExemplar, estado, dataConclusao, observacoes);
        return Ok("Histórico atualizado");
    }

    [HttpDelete("{idHistorico}")]
    public async Task<IActionResult> DeleteHistorico(int idHistorico)
    {
        await _historicoRepository.DeleteHistorico(idHistorico);
        return Ok("Histórico apagado");
    }
}
