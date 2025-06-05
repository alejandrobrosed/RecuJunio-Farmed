using Microsoft.EntityFrameworkCore;
using farmed.Models;
using farmed.Data;

namespace farmed.Repositories;

public class DetalleCompraRepository : IDetalleCompraRepository
{
    private readonly FarmedContext _context;
    public DetalleCompraRepository(FarmedContext context) => _context = context;

    public async Task<IEnumerable<DetalleCompra>> GetByCompraIdAsync(int compraId)
    {
        return await _context.DetalleCompra
            .Include(c => c.medicamento)
            .Where(c => c.compraId == compraId)
            .ToListAsync();
    }

    public async Task<DetalleCompra?> GetByIdAsync(int id)
    {
        return await _context.DetalleCompra.FindAsync(id);
    }

    public async Task<DetalleCompra> AddAsync(DetalleCompra detalle)
    {
        _context.DetalleCompra.Add(detalle);
        await _context.SaveChangesAsync();
        return detalle;
    }

    public async Task<DetalleCompra> UpdateAsync(DetalleCompra detalle)
    {
        var existe = await _context.DetalleCompra.FindAsync(detalle.id);
        if(existe == null) throw new Exception("El detalle no existe");
        existe.cantidad = detalle.cantidad;
        existe.precioUnitario = detalle.precioUnitario;
        await _context.SaveChangesAsync();
        return existe;
    }

}