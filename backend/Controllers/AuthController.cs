using farmed.Models;
using Microsoft.AspNetCore.Mvc;
using farmed.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace farmed.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{

    private readonly IUsuarioService _usuarioService;

    public AuthController(IUsuarioService usuarioServicem)
    {
        _usuarioService = usuarioServicem;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] Usuario usuario)
    {
        var existe = await _usuarioService.GetByCorreoAsync(usuario.correo);
        if (existe != null) return BadRequest("El usuario ya existe");

        // Hash de la contraseña
        usuario.password = BCrypt.Net.BCrypt.HashPassword(usuario.password);
        var nuevo = await _usuarioService.RegisterAsync(usuario);
        return Ok(nuevo);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] Usuario credenciales)
    {
        var existe = await _usuarioService.GetByCorreoAsync(credenciales.correo);
        if (existe == null || !BCrypt.Net.BCrypt.Verify(credenciales.password, existe.password)) return Unauthorized("Credenciales incorrectas");

        HttpContext.Session.SetInt32("UsuarioId", existe.id);
        HttpContext.Session.SetString("Rol", existe.rol);
        var claims = new List<Claim>{
            new Claim(ClaimTypes.NameIdentifier, existe.id.ToString()),
            new Claim(ClaimTypes.Name, existe.correo),
            new Claim(ClaimTypes.Role, existe.rol)
        };
        var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
        var authProperties = new AuthenticationProperties{
            IsPersistent = true,
            ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
        };

        await HttpContext.SignInAsync(
            "Cookies",
            new ClaimsPrincipal(claimsIdentity),
            authProperties
        );
        return Ok(existe);
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return Ok(new { mensaje = "Sesión cerrada" });
    }
}