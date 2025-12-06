using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiTiendaDescuentos.Web.Models
{
    public class Cupo
    {
        [Key]
        public int IdCupo { get; set; }

        public long? IdSede { get; set; }

        public long? IdGrado { get; set; }

        [Display(Name = "Cupos totales")]
        public int CuposTotales { get; set; }

        [Display(Name = "Cupos ocupados")]
        public int CuposOcupados { get; set; }

        [NotMapped]
        [Display(Name = "Disponibles")]
        public int CuposDisponibles => CuposTotales - CuposOcupados;

        public Sede? Sede { get; set; }
        public Grado? Grado { get; set; }
    }
}
