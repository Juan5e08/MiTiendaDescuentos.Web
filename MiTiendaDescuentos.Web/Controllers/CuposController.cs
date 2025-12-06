using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiTiendaDescuentos.Web.Data;
using MiTiendaDescuentos.Web.Models;

namespace MiTiendaDescuentos.Web.Controllers
{
    public class CuposController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CuposController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cupos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cupo.ToListAsync());
        }

        // GET: Cupos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cupo = await _context.Cupo
                .FirstOrDefaultAsync(m => m.IdCupo == id);
            if (cupo == null)
            {
                return NotFound();
            }

            return View(cupo);
        }

        // GET: Cupos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cupos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCupo,IdSede,IdGrado,CuposTotales,CuposOcupados")] Cupo cupo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cupo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cupo);
        }

        // GET: Cupos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cupo = await _context.Cupo.FindAsync(id);
            if (cupo == null)
            {
                return NotFound();
            }
            return View(cupo);
        }

        // POST: Cupos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCupo,IdSede,IdGrado,CuposTotales,CuposOcupados")] Cupo cupo)
        {
            if (id != cupo.IdCupo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cupo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CupoExists(cupo.IdCupo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cupo);
        }

        // GET: Cupos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cupo = await _context.Cupo
                .FirstOrDefaultAsync(m => m.IdCupo == id);
            if (cupo == null)
            {
                return NotFound();
            }

            return View(cupo);
        }

        // POST: Cupos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cupo = await _context.Cupo.FindAsync(id);
            if (cupo != null)
            {
                _context.Cupo.Remove(cupo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CupoExists(int id)
        {
            return _context.Cupo.Any(e => e.IdCupo == id);
        }
    }
}
