using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class MensagemController : ControllerBase
{
    private readonly IMensagemRepository _mensagemRepository;

    public MensagemController(IMensagemRepository mensagemRepository)
    {
        _mensagemRepository = mensagemRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetMensagens()
    {
        var mensagens = await _mensagemRepository.GetMensagens();
        return Ok(mensagens);
    }

    [HttpGet("{idMensagem}")]
    public async Task<IActionResult> GetMensagemById(int idMensagem)
    {
        var mensagem = await _mensagemRepository.GetMensagemById(idMensagem);
        if (mensagem is null)
            return NotFound("Mensagem não encontrada");
        return Ok(mensagem);
    }

    // idHistorico é opcional (pode ser uma mensagem fora de qualquer transação)
    [HttpPost]
    public async Task<IActionResult> InsertMensagem(
        [FromQuery] int     idRemetente,
        [FromQuery] int     idDestinatario,
        [FromQuery] string  conteudo,
        [FromQuery] int?    idHistorico = null)
    {
        await _mensagemRepository.InsertMensagem(idRemetente, idDestinatario, idHistorico, conteudo);
        return Ok("Mensagem enviada");
    }

    // PUT apenas marca a mensagem como lida ou não lida
    [HttpPut("{idMensagem}")]
    public async Task<IActionResult> UpdateMensagem(
        int idMensagem,
        [FromQuery] bool lida)
    {
        await _mensagemRepository.UpdateMensagem(idMensagem, lida);
        return Ok("Mensagem atualizada");
    }

    [HttpDelete("{idMensagem}")]
    public async Task<IActionResult> DeleteMensagem(int idMensagem)
    {
        await _mensagemRepository.DeleteMensagem(idMensagem);
        return Ok("Mensagem apagada");
    }
}
