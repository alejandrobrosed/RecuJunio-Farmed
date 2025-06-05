using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace farmed.Models
{
    [Table("medicamentos")]
    public class Medicamento
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string? nombre { get; set; }
        public string? descripcion { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal precio { get; set; }
        public string? categoria { get; set; }

        public int stock { get; set; }
        public bool disponible { get; set; }
        public string? imagen { get; set; }
        public DateTime fechaAgregado { get; set; } = DateTime.Now;

        public ICollection<DetalleCompra> detallesCompras { get; set; } = new List<DetalleCompra>();
    }
}