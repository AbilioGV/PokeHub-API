using Microsoft.AspNetCore.Mvc;
using PokeHub.API.Models;
using PokeHub.API.Services;

namespace PokeHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    private readonly TokenService _tokenService;

    public AuthController(AuthService authService, TokenService tokenService)
    {
        _authService = authService;
        _tokenService = tokenService;
    }

    [HttpPost("register", Name = "Register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        try
        {
            var user = await _authService.Register(request.Name, request.Email, request.Password, request.IsAdmin);
            return Ok(new { Message = "Usuário registrado com sucesso!", UserId = user.Id });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPost("login", Name = "Login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        try
        {
            var user = await _authService.Login(request.Email, request.Password);

            // Aqui a mágica do JWT acontece após logar com sucesso!
            var token = _tokenService.GenerateToken(user);

            return Ok(new { 
                Token = token,
                User = new { user.Id, user.Name, user.Email, user.IsAdmin }
            });
        }
        catch (ArgumentException ex)
        {
            return Unauthorized(new { Message = ex.Message });
        }
    }
}

// DTOs auxiliares adicionados diretamente aqui para facilitar,
// mas você pode movê-los para a pasta DTOs futuramente.

public class RegisterRequest
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsAdmin { get; set; }
}

public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}
