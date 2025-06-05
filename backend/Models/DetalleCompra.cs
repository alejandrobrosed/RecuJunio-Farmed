using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace farmed.Models
{
    [Table("detalle_compra")]
    public class DetalleCompra
    {
        [Key]
        public int id { get; set; }
        [Required]
        [Column("compra_id")]
        public int compraId { get; set; }
        [ForeignKey("compraId")]
        public Compra? compra { get; set; }
        [Required]
        [Column("medicamento_id")]
        public int medicamentoId { get; set; }
        [ForeignKey("medicamentoId")]
        public Medicamento? medicamento { get; set; }
        [Required]
        public int cantidad { get; set; }
        
        [Column("precio_unitario")]
        public decimal precioUnitario { get; set; }
    }
}