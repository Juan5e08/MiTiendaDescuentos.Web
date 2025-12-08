using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiTiendaDescuentos.Web.Data;
using MiTiendaDescuentos.Web.Models;

namespace MiTiendaDescuentos.Web.Controllers
{
    public class InstitucionesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InstitucionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Instituciones
        // Solo muestra instituciones ACTIVAS
        public async Task<IActionResult> Index()
        {
            var institucionesActivas = await _context.Instituciones
                .Where(i => i.Estado == "Activo")
                .ToListAsync();

            return View(institucionesActivas);
        }

        // GET: Instituciones/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
                return NotFound();

            var institucion = await _context.Instituciones
                .FirstOrDefaultAsync(m => m.IdColegio == id);

            if (institucion == null)
                return NotFound();

            return View(institucion);
        }

        // GET: Instituciones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Instituciones/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Institucion institucion)
        {
            // 1. Validaciones de DataAnnotations
            if (!ModelState.IsValid)
            {
                return View(institucion);
            }

            // 2. Validar que el IdColegio NO exista ya
            var idExiste = await _context.Instituciones
                .AnyAsync(i => i.IdColegio == institucion.IdColegio);

            if (idExiste)
            {
                ModelState.AddModelError(
                    nameof(institucion.IdColegio),
                    "El código de institución ya existe. Por favor ingrese uno diferente."
                );

                return View(institucion);
            }

            // 3. Crear siempre como ACTIVO
            institucion.Estado = "Activo";

            _context.Instituciones.Add(institucion);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Instituciones/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
                return NotFound();

            var institucion = await _context.Instituciones.FindAsync(id);
            if (institucion == null)
                return NotFound();

            return View(institucion);
        }

        // POST: Instituciones/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, Institucion institucion)
        {
            if (id != institucion.IdColegio)
                return NotFound();

            if (!ModelState.IsValid)
            {
                return View(institucion);
            }

            // Cargar la entidad real desde la BD
            var institucionDb = await _context.Instituciones.FindAsync(id);
            if (institucionDb == null)
                return NotFound();

            // Actualizar SOLO los campos editables (no Estado)
            institucionDb.Nombre = institucion.Nombre;
            institucionDb.Localidad = institucion.Localidad;
            institucionDb.DireccionPrincipal = institucion.DireccionPrincipal;
            institucionDb.Correo = institucion.Correo;
            institucionDb.Telefono = institucion.Telefono;
            institucionDb.CantidadSedes = institucion.CantidadSedes;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstitucionExists(id))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Instituciones/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
                return NotFound();

            var institucion = await _context.Instituciones
                .FirstOrDefaultAsync(m => m.IdColegio == id);

            if (institucion == null)
                return NotFound();

            return View(institucion);
        }

        // POST: Instituciones/Delete/5  (BORRADO LÓGICO)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var institucion = await _context.Instituciones.FindAsync(id);
            if (institucion == null)
                return NotFound();

            // Borrado lógico: solo cambiar estado
            institucion.Estado = "Inactivo";
            _context.Update(institucion);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstitucionExists(long id)
        {
            return _context.Instituciones.Any(e => e.IdColegio == id);
        }
    }
}
