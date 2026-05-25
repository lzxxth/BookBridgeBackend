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
        return Ok(mensagem);
    }

    [HttpPost]
    public async Task<IActionResult> InsertMensagem(
        [FromQuery] int idRemetente,
        [FromQuery] int idDestinatario,
        [FromQuery] int? idAnuncio,
        [FromQuery] string conteudo)
    {
        await _mensagemRepository.InsertMensagem(
            idRemetente,
            idDestinatario,
            idAnuncio,
            conteudo);

        return Ok("Mensagem criada");
    }

    [HttpPut("{idMensagem}")]
    public async Task<IActionResult> UpdateMensagem(
        int idMensagem,
        [FromQuery] int idRemetente,
        [FromQuery] int idDestinatario,
        [FromQuery] int? idAnuncio,
        [FromQuery] string conteudo,
        [FromQuery] bool lida)
    {
        await _mensagemRepository.UpdateMensagem(
            idMensagem,
            idRemetente,
            idDestinatario,
            idAnuncio,
            conteudo,
            lida);

        return Ok("Mensagem atualizada");
    }

    [HttpDelete("{idMensagem}")]
    public async Task<IActionResult> DeleteMensagem(int idMensagem)
    {
        await _mensagemRepository.DeleteMensagem(idMensagem);
        return Ok("Mensagem apagada");
    }
}