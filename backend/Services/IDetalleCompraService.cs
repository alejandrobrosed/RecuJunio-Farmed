using farmed.Models;

namespace farmed.Services
{
    public interface IDetalleCompraService
    {
        Task<IEnumerable<DetalleCompra>> GetByCompraIdAsync(int compraId);
        Task<DetalleCompra> UpdateAsync(DetalleCompra detalle);
        Task<DetalleCompra> GetByIdAsync(int id);
        Task<DetalleCompra> CreateAsync(DetalleCompra compra);
        Task<DetalleCompra> CreateDetalleAsync(DetalleCompra detalleCompra);
    }
}