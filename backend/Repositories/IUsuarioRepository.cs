using System.Collections.Generic;
using System.Threading.Tasks;
using farmed.Models;

public interface IUsuarioRepository
{
    public Task<IEnumerable<Usuario>> GetAllAsync();
    public Task<Usuario> GetByIdAsync(int id);
    public Task<Usuario> GetByCorreoAsync(string correo);
    public Task<Usuario> AddAsync(Usuario usuario);
    public Task<Usuario> UpdateAsync(Usuario usuario);
    public Task<bool> DeleteAsync(int id);
}