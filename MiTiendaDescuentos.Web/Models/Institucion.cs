using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiTiendaDescuentos.Web.Models
{
    [Table("instituciones")]
    public class Institucion
    {
        [Key]
        [Column("Id_Colegio")]
        [Required(ErrorMessage = "El código de la institución es obligatorio")]
        [Display(Name = "Código institución")]
        public long IdColegio { get; set; }

        [Column("nombre")]
        [Display(Name = "Nombre")]
        public string? Nombre { get; set; }

        [Column("localidad")]
        [Display(Name = "Localidad")]
        public string? Localidad { get; set; }

        [Column("direccion_principal")]
        [Display(Name = "Dirección principal")]
        public string? DireccionPrincipal { get; set; }

        [Column("correo")]
        [Display(Name = "Correo electrónico")]
        public string? Correo { get; set; }

        [Column("telefono")]
        [Display(Name = "Teléfono")]
        public string? Telefono { get; set; }

        [Column("cantidad_sedes")]
        [Display(Name = "Cantidad de sedes")]
        public int? CantidadSedes { get; set; }

        [Column("Estado")]
        [Display(Name = "Estado")]
        public string? Estado { get; set; }
    }
}
