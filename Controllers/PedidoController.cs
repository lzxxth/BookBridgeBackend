using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PedidoController : ControllerBase
{
    private readonly IPedidoRepository _pedidoRepository;

    public PedidoController(IPedidoRepository pedidoRepository)
    {
        _pedidoRepository = pedidoRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetPedidos()
    {
        var pedidos = await _pedidoRepository.GetPedidos();
        return Ok(pedidos);
    }

    [HttpGet("{idPedido}")]
    public async Task<IActionResult> GetPedidoById(int idPedido)
    {
        var pedido = await _pedidoRepository.GetPedidoById(idPedido);
        if (pedido is null)
            return NotFound("Pedido não encontrado");
        return Ok(pedido);
    }

    // datas opcionais, formato: yyyy-MM-dd
    // BD valida: dataFimPrevista >= dataInicio
    [HttpPost]
    public async Task<IActionResult> InsertPedido(
        [FromQuery] int       idHistorico,
        [FromQuery] DateOnly? dataInicio      = null,
        [FromQuery] DateOnly? dataFimPrevista = null)
    {
        await _pedidoRepository.InsertPedido(idHistorico, dataInicio, dataFimPrevista);
        return Ok("Pedido criado");
    }

    [HttpPut("{idPedido}")]
    public async Task<IActionResult> UpdatePedido(
        int idPedido,
        [FromQuery] int       idHistorico,
        [FromQuery] DateOnly? dataInicio      = null,
        [FromQuery] DateOnly? dataFimPrevista = null,
        [FromQuery] DateOnly? dataFimEfetiva  = null)
    {
        await _pedidoRepository.UpdatePedido(idPedido, idHistorico, dataInicio, dataFimPrevista, dataFimEfetiva);
        return Ok("Pedido atualizado");
    }

    [HttpDelete("{idPedido}")]
    public async Task<IActionResult> DeletePedido(int idPedido)
    {
        await _pedidoRepository.DeletePedido(idPedido);
        return Ok("Pedido apagado");
    }
}
