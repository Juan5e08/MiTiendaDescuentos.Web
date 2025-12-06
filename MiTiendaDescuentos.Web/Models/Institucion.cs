using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiTiendaDescuentos.Web.Models
{
    // Le decimos a EF que esta entidad va a la tabla "instituciones"
    [Table("instituciones")]
    public class Institucion
    {
        [Key]                      // Clave primaria
        [Column("Id_Colegio")]     // Nombre exacto de la columna en la BD
        public long IdColegio { get; set; }

        [Column("nombre")]
        public string? Nombre { get; set; }

        [Column("localidad")]
        public string? Localidad { get; set; }

        [Column("direccion_principal")]
        public string? DireccionPrincipal { get; set; }

        [Column("correo")]
        public string? Correo { get; set; }

        [Column("telefono")]
        public string? Telefono { get; set; }

        [Column("cantidad_sedes")]
        public int? CantidadSedes { get; set; }
    }
}