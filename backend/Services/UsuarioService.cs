using farmed.Models;
using farmed.Repositories;

namespace farmed.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _repo;

    public UsuarioService(IUsuarioRepository repo)
    {
        _repo = repo;
    }

    public Task<IEnumerable<Usuario>> GetAllAsync() => _repo.GetAllAsync();

    public Task<Usuario> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

    public Task<Usuario> GetByCorreoAsync(string correo) => _repo.GetByCorreoAsync(correo);
    public Task<Usuario> RegisterAsync(Usuario usuario) => _repo.AddAsync(usuario);
    public Task<Usuario> UpdateAsync(Usuario usuario) => _repo.UpdateAsync(usuario);
    public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);
}
