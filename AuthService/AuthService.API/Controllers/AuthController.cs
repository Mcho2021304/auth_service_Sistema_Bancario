using AuthService.Application.DTOs;
using AuthService.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthService.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController(AuthenticationService authService) : ControllerBase
{
    /// <summary>
    /// Procesa la solicitud HTTP para registrar un nuevo usuario en el sistema, asegurando que la creación de perfiles administrativos exija un token con privilegios.
    /// </summary>
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto request)
    {
        try
        {
            if (request.Role == "Admin")
            {
                var currentUserRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (currentUserRole != "Admin")
                {
                    return StatusCode(403, new { Message = "Se requiere un token de administrador válido para registrar usuarios con este rol." });
                }
            }

            var result = await authService.RegisterAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    /// <summary>
    /// Procesa la solicitud HTTP para autenticar un usuario y retornar su token JWT.
    /// </summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto request)
    {
        try
        {
            var result = await authService.LoginAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return Unauthorized(new { Message = ex.Message });
        }
    }
}