using farmed.Models;

public interface IUsuarioService
{
    Task<IEnumerable<Usuario>> GetAllAsync();
    Task<Usuario> GetByIdAsync(int id);
    Task<Usuario> GetByCorreoAsync(string correo);
    Task<Usuario> RegisterAsync(Usuario usuario);
    Task<Usuario> UpdateAsync(Usuario usuario);
    Task<bool> DeleteAsync(int id);
}