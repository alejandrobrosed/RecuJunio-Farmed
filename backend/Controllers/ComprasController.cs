using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using farmed.Models;
using farmed.Services;

namespace farmed.Controllers;

[Route("api/compras")]
[ApiController]
[Authorize]

public class ComprasController : ControllerBase
{
    private readonly ICompraService _compraService;
    private readonly IDetalleCompraService _detalleCompraService;

    public ComprasController(ICompraService compraService, IDetalleCompraService detalleCompraService)
    {
        _compraService = compraService;
        _detalleCompraService = detalleCompraService;
    }

    [HttpGet]
    public async Task<IActionResult> GetComprasUsuario()
    {
        var usuarioIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if(usuarioIdClaim == null) return Unauthorized("Sesión no válida");
        var usuarioId = int.Parse(usuarioIdClaim);
        var compras = await _compraService.GetByUsuarioIdAsync(usuarioId);
        return Ok(compras);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var compra = await _compraService.GetByIdAsync(id);
        return compra == null ? NotFound() : Ok(compra);
    }

    // [HttpPost]
    // public async Task<IActionResult> Create([FromBody] Compra compra)
    // {
    //     var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
    //     compra.usuarioId = usuarioId;
    //     compra.fecha = DateTime.Now;
    //     var nuevaCompra = await _compraService.CreateAsync(compra);
    //     return CreatedAtAction(nameof(GetById), new { id = nuevaCompra.id }, nuevaCompra);
    // }

    [HttpPost("finalizar")]
    public async Task<IActionResult> Create([FromBody] CompraRequest request)
    {
        try
        {
            var usuarioIdstr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (usuarioIdstr == null) return Unauthorized("Sesión no válida");

            var usuarioId = int.Parse(usuarioIdstr);
            var total = request.Productos.Sum(p => p.Precio * p.Cantidad);
            var compra = new Compra
            {
                usuarioId = usuarioId,
                fecha = DateTime.Now,
                total = total
            };
            var nuevaCompra = await _compraService.CreateAsync(compra);

            foreach (var detalle in request.Productos)
            {
                var detalleCompra = new DetalleCompra
                {
                    compraId = nuevaCompra.id,
                    medicamentoId = detalle.MedicamentoId,
                    cantidad = detalle.Cantidad,
                    precioUnitario = detalle.Precio
                };
                await _detalleCompraService.CreateDetalleAsync(detalleCompra);
            }

            return CreatedAtAction(nameof(GetById), new { id = nuevaCompra.id }, nuevaCompra);
        } catch (DbUpdateException dbex) {
            return StatusCode(500, "Error al guardar en la bbdd: " + dbex.InnerException?.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Error al crear la compra: " + ex.Message);
        }

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var compra = await _compraService.GetByIdAsync(id);
        if (compra == null) return NotFound();
        return Ok(await _compraService.DeleteAsync(id));
    }





}