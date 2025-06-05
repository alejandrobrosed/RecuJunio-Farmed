using Microsoft.EntityFrameworkCore;
using farmed.Models;
using farmed.Data;

namespace farmed.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly FarmedContext _context;
    public UsuarioRepository(FarmedContext context) => _context = context;

    public async Task<IEnumerable<Usuario>> GetAllAsync() => await _context.Usuarios.ToListAsync();

    public async Task<Usuario> GetByIdAsync(int id) => await _context.Usuarios.FindAsync(id);
    public async Task<Usuario> GetByCorreoAsync(string correo) => await _context.Usuarios.FirstOrDefaultAsync(u => u.correo == correo);
    public async Task<Usuario> AddAsync(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        return usuario;
    }
    public async Task<Usuario> UpdateAsync(Usuario usuario)
    {
        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync();
        return usuario;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null) return false;
        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();
        return true;
    }
}