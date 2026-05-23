using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TipoTransacaoController : ControllerBase
{
    private readonly ITipoTransacaoRepository _tipoTransacaoRepository;

    public TipoTransacaoController(ITipoTransacaoRepository tipoTransacaoRepository)
    {
        _tipoTransacaoRepository = tipoTransacaoRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetTipoTransacoes()
    {
        var tipos = await _tipoTransacaoRepository.GetTipoTransacoes();
        return Ok(tipos);
    }

    [HttpGet("{idTipoTransacao}")]
    public async Task<IActionResult> GetTipoTransacaoById(int idTipoTransacao)
    {
        var tipo = await _tipoTransacaoRepository.GetTipoTransacaoById(idTipoTransacao);
        if (tipo is null)
            return NotFound("TipoTransacao não encontrado");
        return Ok(tipo);
    }

    [HttpPost]
    public async Task<IActionResult> InsertTipoTransacao([FromQuery] string descricao)
    {
        await _tipoTransacaoRepository.InsertTipoTransacao(descricao);
        return Ok("TipoTransacao criado");
    }

    [HttpPut("{idTipoTransacao}")]
    public async Task<IActionResult> UpdateTipoTransacao(int idTipoTransacao, [FromQuery] string descricao)
    {
        await _tipoTransacaoRepository.UpdateTipoTransacao(idTipoTransacao, descricao);
        return Ok("TipoTransacao atualizado");
    }

    [HttpDelete("{idTipoTransacao}")]
    public async Task<IActionResult> DeleteTipoTransacao(int idTipoTransacao)
    {
        await _tipoTransacaoRepository.DeleteTipoTransacao(idTipoTransacao);
        return Ok("TipoTransacao apagado");
    }
}
