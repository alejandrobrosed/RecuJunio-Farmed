using farmed.Models;

public interface ICompraService
{
    Task<IEnumerable<Compra>> GetByUsuarioIdAsync(int usuarioId);
    Task<Compra> GetByIdAsync(int id);
    Task<Compra> CreateAsync(Compra Compra);
    Task<bool> DeleteAsync(int id);
}