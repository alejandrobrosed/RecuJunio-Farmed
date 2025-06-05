using System.Collections.Generic;
using System.Threading.Tasks;
using farmed.Models;

public interface ICompraRepository
{
    public Task<IEnumerable<Compra>> GetByUsuarioIdAsync(int id);
    public Task<Compra> GetByIdAsync(int id);
    public Task<Compra> AddAsync(Compra compra);
    public Task<bool> DeleteAsync(int id);
}