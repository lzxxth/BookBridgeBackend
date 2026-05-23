using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userRepository.GetUsers();
        return Ok(users);
    }

    [HttpGet("{idUser}")]
    public async Task<IActionResult> GetUserById(int idUser)
    {
        var user = await _userRepository.GetUserById(idUser);
        if (user is null)
            return NotFound("Utilizador não encontrado");
        return Ok(user);
    }

    // curso e instituicao são opcionais (NULL na BD)
    [HttpPost]
    public async Task<IActionResult> InsertUser(
        [FromQuery] string  nome,
        [FromQuery] string  email,
        [FromQuery] string  passwordHash,
        [FromQuery] int     idTipoUser,
        [FromQuery] string? curso       = null,
        [FromQuery] string? instituicao = null,
        [FromQuery] bool    ativo       = true)
    {
        await _userRepository.InsertUser(nome, email, passwordHash, idTipoUser, curso, instituicao, ativo);
        return Ok("Utilizador criado");
    }

    [HttpPut("{idUser}")]
    public async Task<IActionResult> UpdateUser(
        int idUser,
        [FromQuery] string  nome,
        [FromQuery] string  email,
        [FromQuery] string  passwordHash,
        [FromQuery] int     idTipoUser,
        [FromQuery] string? curso       = null,
        [FromQuery] string? instituicao = null,
        [FromQuery] bool    ativo       = true)
    {
        await _userRepository.UpdateUser(idUser, nome, email, passwordHash, idTipoUser, curso, instituicao, ativo);
        return Ok("Utilizador atualizado");
    }

    [HttpDelete("{idUser}")]
    public async Task<IActionResult> DeleteUser(int idUser)
    {
        await _userRepository.DeleteUser(idUser);
        return Ok("Utilizador apagado");
    }
}
