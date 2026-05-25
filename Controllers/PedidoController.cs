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
        return Ok(pedido);
    }

    [HttpPost]
    public async Task<IActionResult> InsertPedido(
        [FromQuery] int idUser,
        [FromQuery] int idLivro,
        [FromQuery] string? descricao)
    {
        await _pedidoRepository.InsertPedido(idUser, idLivro, descricao);
        return Ok("Pedido criado");
    }

    [HttpPut("{idPedido}")]
    public async Task<IActionResult> UpdatePedido(
        int idPedido,
        [FromQuery] int idUser,
        [FromQuery] int idLivro,
        [FromQuery] string? descricao,
        [FromQuery] string estado)
    {
        await _pedidoRepository.UpdatePedido(idPedido, idUser, idLivro, descricao, estado);
        return Ok("Pedido atualizado");
    }

    [HttpDelete("{idPedido}")]
    public async Task<IActionResult> DeletePedido(int idPedido)
    {
        await _pedidoRepository.DeletePedido(idPedido);
        return Ok("Pedido apagado");
    }
}