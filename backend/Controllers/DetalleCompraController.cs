using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using farmed.Models;
using farmed.Services;

namespace farmed.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class DetalleCompraController : ControllerBase
{
    private readonly IDetalleCompraService _detalleCompraService;

    public DetalleCompraController(IDetalleCompraService detalleCompraService)
    {
        _detalleCompraService = detalleCompraService;
    }

    [HttpGet("{compraId}")]
    public async Task<IActionResult> GetDetalles(int compraId)
    {
        var detalleCompra = await _detalleCompraService.GetByCompraIdAsync(compraId);
        return detalleCompra == null ? NotFound() : Ok(detalleCompra);
    }

    [HttpPost] // Actualizar
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] DetalleCompra detalleCompra)
    {
        var creado = await _detalleCompraService.CreateAsync(detalleCompra);
        return Ok(creado);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] DetalleCompra actualizado)
    {   
        Console.WriteLine("---------------------------");
        Console.WriteLine(actualizado.cantidad);
        Console.WriteLine(actualizado.precioUnitario);

        

        var compra = await _detalleCompraService.GetByIdAsync(id);
        if (compra == null) return NotFound();

        Console.WriteLine("---------------------------");
        Console.WriteLine(compra.precioUnitario);

        compra.cantidad = actualizado.cantidad;
        compra.precioUnitario = actualizado.precioUnitario;

        var actualizadoFinal = await _detalleCompraService.UpdateAsync(compra);
        return Ok(actualizadoFinal);
    }

}