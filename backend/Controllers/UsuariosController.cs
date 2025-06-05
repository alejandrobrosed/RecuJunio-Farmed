using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using farmed.Models;
using farmed.Services;

namespace farmed.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]


public class UsuariosController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuariosController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync() => Ok(await _usuarioService.GetAllAsync());

    [HttpGet("me")]
    public async Task<IActionResult> GetPerfil()
    {
        var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var usuario = await _usuarioService.GetByIdAsync(id);
        return usuario == null ? NotFound() : Ok(usuario);
    }

    // [HttpPut]
    // public async Task<IActionResult> UpdatePerfil([FromBody] Usuario actualizado)
    // {
    //     var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
    //     var usuario = await _usuarioService.GetByIdAsync(id);
    //     if (usuario == null) return NotFound();

    //     usuario.nombre = actualizado.nombre;
    //     usuario.activo = actualizado.activo;
    //     var resultado = await _usuarioService.UpdateAsync(usuario);
    //     return Ok(resultado);
    // }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUsuario(int id, [FromBody] Usuario actualizado)
    {
        var usuario = await _usuarioService.GetByIdAsync(id);
        if (usuario == null) return NotFound();
        usuario.nombre = actualizado.nombre;
        usuario.correo = actualizado.correo;
        usuario.rol = actualizado.rol;
        usuario.activo = actualizado.activo;
        await _usuarioService.UpdateAsync(usuario);
        return Ok(usuario);
    }

    [HttpDelete("{id}")] // Eliminar
    public async Task<IActionResult> DeleteUsuario(int id)
    {
        var usuario = await _usuarioService.GetByIdAsync(id);
        if (usuario == null) return NotFound();
        await _usuarioService.DeleteAsync(id);
        return NoContent();
    }
}