using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiTiendaDescuentos.Web.Models;

namespace MiTiendaDescuentos.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets para las tablas de la BD "colegios"
        public DbSet<Cita> Cita { get; set; }
        public DbSet<Cupo> Cupo { get; set; }
        public DbSet<Grado> Grado { get; set; }
        public DbSet<Institucion> Instituciones { get; set; }   // <- IMPORTANTE
        public DbSet<Sede> Sede { get; set; }
    }
}
