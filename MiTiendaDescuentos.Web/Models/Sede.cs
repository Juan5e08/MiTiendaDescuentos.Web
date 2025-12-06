using System.ComponentModel.DataAnnotations;

namespace MiTiendaDescuentos.Web.Models
{
    public class Sede
    {
        [Key]
        public long IdSede { get; set; }

        [Display(Name = "Institución")]
        public long? IdColegio { get; set; }

        [Required, StringLength(50)]
        public string? Nombre { get; set; }

        [StringLength(255)]
        public string? Direccion { get; set; }

        [StringLength(20)]
        public string? Telefono { get; set; }

        [StringLength(100), EmailAddress]
        public string? Correo { get; set; }

        // Relación
        public Institucion? Institucion { get; set; }
    }
}
