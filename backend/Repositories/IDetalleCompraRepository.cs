using System.Collections.Generic;
using System.Threading.Tasks;
using farmed.Models;

public interface IDetalleCompraRepository
{
    public Task<IEnumerable<DetalleCompra>> GetByCompraIdAsync(int compraId);
    public Task<DetalleCompra?> GetByIdAsync(int id);
    public Task<DetalleCompra> AddAsync(DetalleCompra detalle);

    public Task<DetalleCompra> UpdateAsync(DetalleCompra detalle);
}