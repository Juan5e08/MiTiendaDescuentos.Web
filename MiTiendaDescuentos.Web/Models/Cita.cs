using System.ComponentModel.DataAnnotations;

namespace MiTiendaDescuentos.Web.Models
{
    public class Cita
    {
        [Key]
        public long IdCita { get; set; }

        [Display(Name = "Fecha de la cita")]
        public DateTime FechaCita { get; set; }

        [Display(Name = "Hora")]
        public TimeSpan HoraCita { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Agendado por")]
        public string NombreAgenda { get; set; } = string.Empty;


        [StringLength(100), EmailAddress]
        public string? CorreoAgenda { get; set; }

        [StringLength(20)]
        public string? TelefonoAgenda { get; set; }

        public int CantidadCitas { get; set; }

        [Display(Name = "Institución")]
        public long IdColegio { get; set; }

        [Display(Name = "Sede")]
        public int IdSede { get; set; }

        [StringLength(20)]
        public string? Estado { get; set; }

        public Institucion? Institucion { get; set; }
        public Sede? Sede { get; set; }
    }
}