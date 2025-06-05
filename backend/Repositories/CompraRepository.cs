using Microsoft.EntityFrameworkCore;
using farmed.Models;
using farmed.Data;

namespace farmed.Repositories;

public class CompraRepository : ICompraRepository
{
    private readonly FarmedContext _context;
    public CompraRepository(FarmedContext context) => _context = context;

    public async Task<IEnumerable<Compra>> GetByUsuarioIdAsync(int usuarioId)
    {
        return await _context.Compras
            .Include(c => c.detalles)
            .ThenInclude(d => d.medicamento)
            .Where(c => c.usuarioId == usuarioId)
            .ToListAsync();
    }

    public async Task<Compra> GetByIdAsync(int id)
    {
        return await _context.Compras
            .Include(c => c.detalles)
            .ThenInclude(d => d.medicamento)
            .FirstOrDefaultAsync(c => c.id == id);
    }
    public async Task<Compra> AddAsync(Compra compra)
    {
        _context.Compras.Add(compra);
        await _context.SaveChangesAsync();
        return compra;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var compra = await _context.Compras.FindAsync(id);
        if (compra == null) return false;
        _context.Compras.Remove(compra);
        await _context.SaveChangesAsync();
        return true;
    }
}