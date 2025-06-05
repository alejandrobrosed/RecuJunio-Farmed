using Microsoft.EntityFrameworkCore;
using farmed.Models;
using farmed.Data;

namespace farmed.Repositories;

public class MedicamentoRepository : IMedicamentoRepository
{
    private readonly FarmedContext _context;
    public MedicamentoRepository(FarmedContext context) => _context = context;

    public async Task<IEnumerable<Medicamento>> GetAllAsync() => await _context.Medicamentos.ToListAsync();

    public async Task<Medicamento> GetByIdAsync(int id) => await _context.Medicamentos.FindAsync(id);
    public async Task<Medicamento> AddAsync(Medicamento medicamento)
    {
        _context.Medicamentos.Add(medicamento);
        await _context.SaveChangesAsync();
        return medicamento;
    }
    public async Task<Medicamento> UpdateAsync(Medicamento medicamento)
    {

        var existe = await _context.Medicamentos.FindAsync(medicamento.id);
        if (existe == null)
            throw new Exception("El medicamento no existe");

        _context.Entry(existe).CurrentValues.SetValues(medicamento);
        await _context.SaveChangesAsync();
        return existe;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var medicamento = await _context.Medicamentos.FindAsync(id);
        if (medicamento == null) return false;
        _context.Medicamentos.Remove(medicamento);
        await _context.SaveChangesAsync();
        return true;
    }
}