using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using farmed.Models;
using farmed.Services;

namespace farmed.Controllers;

[Route("api/[controller]")]
[ApiController]



public class MedicamentosController : ControllerBase
{
    private readonly IMedicamentoService _medicamentoService;

    public MedicamentosController(IMedicamentoService medicamentoService)
    {
        _medicamentoService = medicamentoService;
    }

    //ZONA PUBLICA
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        var medicamentos = await _medicamentoService.GetAllAsync();
        return Ok(medicamentos);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(int id)
    {
        var medicamento = await _medicamentoService.GetByIdAsync(id);
        return medicamento == null ? NotFound() : Ok(medicamento);
    }

    //ZONA DE ADMIN
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] Medicamento medicamento)
    {
        var nuevo = await _medicamentoService.CreateAsync(medicamento);
        return CreatedAtAction(nameof(GetById), new { id = nuevo.id }, nuevo);
    }
    [HttpPut("{id}")] // Actualizar

    public async Task<IActionResult> Update(int id, [FromBody] Medicamento medicamento)
    {
        var existe = await _medicamentoService.GetByIdAsync(id);
        if (existe == null) return NotFound();
        medicamento.id = id;
        return Ok(await _medicamentoService.UpdateAsync(medicamento));
    }

    [HttpDelete("{id}")] // Eliminar

    public async Task<IActionResult> Delete(int id)
    {
        var existe = await _medicamentoService.GetByIdAsync(id);
        if (existe == null) return NotFound();
        return Ok(await _medicamentoService.DeleteAsync(id));
    }

    [HttpGet("categorias")]
    [AllowAnonymous]
    public async Task<IActionResult> GetCategorias()
    {
        var medicamentos = await _medicamentoService.GetAllAsync();
        var categorias = medicamentos.Where(m => m.categoria != null).Select(m => m.categoria).Distinct().OrderBy(c => c);
        return Ok(categorias);
    }

    [HttpGet("destacados")]
    [AllowAnonymous]
    public async Task<IActionResult> GetDestacados()
    {
        var medicamentos = await _medicamentoService.GetAllAsync();
        var random = new Random();
        var destacados = medicamentos.OrderBy(x => random.Next()).Take(4).Select(medicamentos => new
        {
            id = medicamentos.id,
            nombre = medicamentos.nombre,
            descripcion = medicamentos.descripcion,
            precio = medicamentos.precio,
            categoria = medicamentos.categoria,
            imagen = medicamentos.imagen
        });
        return Ok(destacados);
    }

}