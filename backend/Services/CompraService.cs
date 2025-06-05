using farmed.Models;
using farmed.Repositories;

namespace farmed.Services;

public class CompraService : ICompraService
{
    private readonly ICompraRepository _repo;
    public CompraService(ICompraRepository repo)
    {
        _repo = repo;
    }
    public Task<IEnumerable<Compra>> GetByUsuarioIdAsync(int usuarioId) => _repo.GetByUsuarioIdAsync(usuarioId);
    public Task<Compra> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
    public Task<Compra> CreateAsync(Compra compra) => _repo.AddAsync(compra);
    public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);
}