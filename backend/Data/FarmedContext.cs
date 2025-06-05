using Microsoft.EntityFrameworkCore;
using farmed.Models;

namespace farmed.Data
{
    public class FarmedContext : DbContext
    {
        public FarmedContext(DbContextOptions<FarmedContext> options) : base(options) { }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Medicamento> Medicamentos { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<DetalleCompra> DetalleCompra { get; set; }

    }
}
