using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace farmed.Models
{
    [Table("compras")]
    public class Compra
    {
        [Key]
        public int id { get; set; }

        [Required]
        [Column("usuario_id")]
        public int usuarioId { get; set; }
        [ForeignKey("usuarioId")]
        public Usuario? usuario { get; set; }
        public DateTime fecha { get; set; } = DateTime.Now;
        [Column(TypeName = "decimal(10,2)")]
        public decimal? total { get; set; }
        public bool pagado { get; set; } = false;
        public ICollection<DetalleCompra> detalles { get; set; } = new List<DetalleCompra>();
    }
}