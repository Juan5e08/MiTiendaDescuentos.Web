using System.ComponentModel.DataAnnotations;

namespace MiTiendaDescuentos.Web.Models
{
    public class Grado
    {
        [Key]
        public long IdGrado { get; set; }

        public long? IdSede { get; set; }

        [Display(Name = "Nombre del grado")]
        [StringLength(50)]
        public string? NombreGrado { get; set; }

        public Sede? Sede { get; set; }
    }
}
