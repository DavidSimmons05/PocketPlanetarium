using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PocketPlanetarium.Data;
using PocketPlanetarium.Models;

namespace PocketPlanetarium.Controllers
{
    public class PlanetariumsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlanetariumsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Planetariums
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Planetarium.Include(p => p.user);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Planetariums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planetarium = await _context.Planetarium
                .Include(p => p.user)
                .FirstOrDefaultAsync(m => m.planetariumId == id);
            if (planetarium == null)
            {
                return NotFound();
            }

            return View(planetarium);
        }

        // GET: Planetariums/Create
        public IActionResult Create()
        {
            ViewData["userId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Planetariums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("planetariumId,userId")] Planetarium planetarium)
        {
            if (ModelState.IsValid)
            {
                _context.Add(planetarium);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["userId"] = new SelectList(_context.Users, "Id", "Id", planetarium.userId);
            return View(planetarium);
        }

        // GET: Planetariums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planetarium = await _context.Planetarium.FindAsync(id);
            if (planetarium == null)
            {
                return NotFound();
            }
            ViewData["userId"] = new SelectList(_context.Users, "Id", "Id", planetarium.userId);
            return View(planetarium);
        }

        // POST: Planetariums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("planetariumId,userId")] Planetarium planetarium)
        {
            if (id != planetarium.planetariumId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(planetarium);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanetariumExists(planetarium.planetariumId))
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
            ViewData["userId"] = new SelectList(_context.Users, "Id", "Id", planetarium.userId);
            return View(planetarium);
        }

        // GET: Planetariums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planetarium = await _context.Planetarium
                .Include(p => p.user)
                .FirstOrDefaultAsync(m => m.planetariumId == id);
            if (planetarium == null)
            {
                return NotFound();
            }

            return View(planetarium);
        }

        // POST: Planetariums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var planetarium = await _context.Planetarium.FindAsync(id);
            if (planetarium != null)
            {
                _context.Planetarium.Remove(planetarium);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlanetariumExists(int id)
        {
            return _context.Planetarium.Any(e => e.planetariumId == id);
        }
    }
}
