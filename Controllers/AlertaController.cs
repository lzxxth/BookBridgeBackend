using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AlertaController : ControllerBase
{
    private readonly IAlertaRepository _alertaRepository;

    public AlertaController(IAlertaRepository alertaRepository)
    {
        _alertaRepository = alertaRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAlertas()
    {
        var alertas = await _alertaRepository.GetAlertas();
        return Ok(alertas);
    }

    [HttpGet("{idAlerta}")]
    public async Task<IActionResult> GetAlertaById(int idAlerta)
    {
        var alerta = await _alertaRepository.GetAlertaById(idAlerta);
        return Ok(alerta);
    }

    [HttpPost]
    public async Task<IActionResult> InsertAlerta(
        [FromQuery] int idUser,
        [FromQuery] int? idAnuncio,
        [FromQuery] int? idPedido,
        [FromQuery] string mensagem)
    {
        await _alertaRepository.InsertAlerta(
            idUser,
            idAnuncio,
            idPedido,
            mensagem);

        return Ok("Alerta criado");
    }

    [HttpPut("{idAlerta}")]
    public async Task<IActionResult> UpdateAlerta(
        int idAlerta,
        [FromQuery] int idUser,
        [FromQuery] int? idAnuncio,
        [FromQuery] int? idPedido,
        [FromQuery] string mensagem,
        [FromQuery] bool lido)
    {
        await _alertaRepository.UpdateAlerta(
            idAlerta,
            idUser,
            idAnuncio,
            idPedido,
            mensagem,
            lido);

        return Ok("Alerta atualizado");
    }

    [HttpDelete("{idAlerta}")]
    public async Task<IActionResult> DeleteAlerta(int idAlerta)
    {
        await _alertaRepository.DeleteAlerta(idAlerta);
        return Ok("Alerta apagado");
    }
}