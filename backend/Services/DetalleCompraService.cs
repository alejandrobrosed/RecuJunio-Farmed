using farmed.Models;
using farmed.Repositories;
namespace farmed.Services;

public class DetalleCompraService : IDetalleCompraService
{
    private readonly IDetalleCompraRepository _repo;
    public DetalleCompraService(IDetalleCompraRepository repo)
    {
        _repo = repo;
    }
    public Task<IEnumerable<DetalleCompra>> GetByCompraIdAsync(int compraId) => _repo.GetByCompraIdAsync(compraId);

    public Task<DetalleCompra> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
    public Task<DetalleCompra> CreateAsync(DetalleCompra compra) => _repo.AddAsync(compra);
    public Task<DetalleCompra> CreateDetalleAsync(DetalleCompra detalle) => _repo.AddAsync(detalle);

    public Task<DetalleCompra> UpdateAsync(DetalleCompra detalle) => _repo.UpdateAsync(detalle);
    
    
    // public async Task<DetalleCompra> CreateDetalleAsync(DetalleCompra detalleCompra)
    // {
    //     return await _repo.AddAsync(detalleCompra);
    // }


}