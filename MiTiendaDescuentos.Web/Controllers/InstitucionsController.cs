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
        public async Task<IActionResult> Index()
        {
            var lista = await _context.Instituciones.ToListAsync();
            return View(lista);
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
        public async Task<IActionResult> Create([Bind("IdColegio,Nombre,Localidad,DireccionPrincipal,Correo,Telefono,CantidadSedes")] Institucion institucion)
        {
            if (!ModelState.IsValid)
                return View(institucion);

            _context.Add(institucion);
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
        public async Task<IActionResult> Edit(long id, [Bind("IdColegio,Nombre,Localidad,DireccionPrincipal,Correo,Telefono,CantidadSedes")] Institucion institucion)
        {
            if (id != institucion.IdColegio)
                return NotFound();

            if (!ModelState.IsValid)
                return View(institucion);

            try
            {
                _context.Update(institucion);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstitucionExists(institucion.IdColegio))
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

        // POST: Instituciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var institucion = await _context.Instituciones.FindAsync(id);
            if (institucion == null)
                return NotFound();

            // <<< AQUÍ está la clave: revisar si tiene sedes asociadas >>>
            // Ojo: en tu ApplicationDbContext la DbSet se llama "Sede"
            bool tieneSedes = await _context.Sede
                .AnyAsync(s => s.IdColegio == id);

            if (tieneSedes)
            {
                ModelState.AddModelError(string.Empty,
                    "No se puede eliminar la institución porque tiene sedes asociadas. " +
                    "Elimina primero las sedes relacionadas.");

                // Volvemos a mostrar la vista Delete con el mensaje de error
                return View("Delete", institucion);
            }

            _context.Instituciones.Remove(institucion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstitucionExists(long id)
        {
            return _context.Instituciones.Any(e => e.IdColegio == id);
        }
    }
}
