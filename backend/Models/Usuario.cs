using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace farmed.Models
{
    [Table("usuarios")]
    public class Usuario
    {
        [Key]
        public int id { get; set; }
        public string? nombre { get; set; }
        public string? correo { get; set; }
        public string? password { get; set; }

        [Required]
        public string rol { get; set; } = "usuario";

        public bool activo { get; set; }

        public DateTime fechaRegistro { get; set; } = DateTime.Now;

        public ICollection<Compra> compras { get; set; } = new List<Compra>();
    }
}