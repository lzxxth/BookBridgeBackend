using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TipoUserController : ControllerBase
{
    private readonly ITipoUserRepository _tipoUserRepository;

    public TipoUserController(ITipoUserRepository tipoUserRepository)
    {
        _tipoUserRepository = tipoUserRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetTipoUsers()
    {
        var tipoUsers = await _tipoUserRepository.GetTipoUsers();
        return Ok(tipoUsers);
    }

    [HttpGet("{idTipoUser}")]
    public async Task<IActionResult> GetTipoUserById(int idTipoUser)
    {
        var tipoUser = await _tipoUserRepository.GetTipoUserById(idTipoUser);
        if (tipoUser is null)
            return NotFound("TipoUser não encontrado");
        return Ok(tipoUser);
    }

    [HttpPost]
    public async Task<IActionResult> InsertTipoUser(
        [FromQuery] string descricao)
    {
        await _tipoUserRepository.InsertTipoUser(descricao);
        return Ok("TipoUser criado");
    }

    [HttpPut("{idTipoUser}")]
    public async Task<IActionResult> UpdateTipoUser(
        int idTipoUser,
        [FromQuery] string descricao)
    {
        await _tipoUserRepository.UpdateTipoUser(idTipoUser, descricao);
        return Ok("TipoUser atualizado");
    }

    [HttpDelete("{idTipoUser}")]
    public async Task<IActionResult> DeleteTipoUser(int idTipoUser)
    {
        await _tipoUserRepository.DeleteTipoUser(idTipoUser);
        return Ok("TipoUser apagado");
    }
}
