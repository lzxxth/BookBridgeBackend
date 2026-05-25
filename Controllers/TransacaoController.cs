using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/[controller]")]
public class TransacaoController : ControllerBase
{
  private readonly ITransacaoRepository _transacaoRepository;
  public TransacaoController(ITransacaoRepository transacaoRepository) {
    _transacaoRepository = transacaoRepository;
  }
  [HttpGet]
  public async Task<IActionResult> GetTransacoes() {
    var transacoes = await _transacaoRepository.GetTransacoes();
    return Ok(transacoes);
  }
  [HttpGet("{idTransacao}")]
  public async Task<IActionResult> GetTransacaoById(int idTransacao) {
    var transacao = await _transacaoRepository.GetTransacaoById(idTransacao);
    return Ok(transacao);
  }
  [HttpGet("anuncio/{idAnuncio}")]
  public async Task<IActionResult> GetTransacoesByAnuncio(int idAnuncio) {
    var transacoes = await _transacaoRepository.GetTransacoesByAnuncio(idAnuncio);
    return Ok(transacoes);
  }
  [HttpGet("comprador/{idUserComprador}")]
  public async Task<IActionResult> GetTransacoesByComprador(int idUserComprador) {
    var transacoes = await _transacaoRepository.GetTransacoesByComprador(idUserComprador);
    return Ok(transacoes);
  }
  [HttpPost]
  public async Task<IActionResult> InsertTransacao(
      [FromQuery] int       idAnuncio,
      [FromQuery] int       idUserComprador,
      [FromQuery] DateTime? dataFimPrevista,
      [FromQuery] string?   observacoes)
  {
    await _transacaoRepository.InsertTransacao(idAnuncio, idUserComprador, dataFimPrevista, observacoes);
    return Ok("Transacao criada");
  }
  [HttpPut("{idTransacao}")]
  public async Task<IActionResult> UpdateTransacao(
      int               idTransacao,
      [FromQuery] string    estado,
      [FromQuery] DateTime? dataFimPrevista,
      [FromQuery] DateTime? dataFimEfetiva,
      [FromQuery] string?   observacoes)
  {
    await _transacaoRepository.UpdateTransacao(idTransacao, estado, dataFimPrevista, dataFimEfetiva, observacoes);
    return Ok("Transacao atualizada");
  }
  [HttpDelete("{idTransacao}")]
  public async Task<IActionResult> DeleteTransacao(int idTransacao) {
    await _transacaoRepository.DeleteTransacao(idTransacao);
    return Ok("Transacao apagada");
  }

  [HttpGet("dias/{idTransacao}")]
  public async Task<IActionResult> GetDiasTransacao(int idTransacao)
  {
      var dias = await _transacaoRepository.GetDiasTransacao(idTransacao);
      return Ok(dias);
  }

  [HttpGet("ativas")]
  public async Task<IActionResult> GetTransacoesAtivas()
  {
      var result = await _transacaoRepository.GetTransacoesAtivas();
      return Ok(result);
  }

  [HttpGet("indicadores")]
  public async Task<IActionResult> GetIndicadores()
  {
      var result = await _transacaoRepository.GetIndicadores();
      return Ok(result);
  }

  [HttpPost("iniciar")]
  public async Task<IActionResult> IniciarTransacao(
      [FromQuery] int idAnuncio,
      [FromQuery] int idUserComprador,
      [FromQuery] DateTime? dataFimPrevista)
  {
      var id = await _transacaoRepository.IniciarTransacao(
          idAnuncio,
          idUserComprador,
          dataFimPrevista
      );

      return Ok(new { idTransacao = id });
  }

  [HttpPut("atualizar")]
  public async Task<IActionResult> AtualizarTransacao(
      [FromQuery] int idTransacao,
      [FromQuery] string novoEstado,
      [FromQuery] DateTime? dataFimEfetiva)
  {
      await _transacaoRepository.AtualizarTransacao(
          idTransacao,
          novoEstado,
          dataFimEfetiva
      );

      return Ok("Transação atualizada");
  }
}
