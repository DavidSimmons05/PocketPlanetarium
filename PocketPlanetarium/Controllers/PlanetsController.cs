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
    public class PlanetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlanetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Planets
        public async Task<IActionResult> Index()
        {
            return View("Index", await _context.Planet.ToListAsync());
        }

        // GET: Planets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return View("404");
            }

            var planet = await _context.Planet
                .FirstOrDefaultAsync(m => m.planetId == id);
            if (planet == null)
            {
                return View("404");
            }

            return View("Details", planet);
        }

        // GET: Planets/Create
        public IActionResult Create()
        {
            return View("Create");
        }

        // POST: Planets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("planetId,Name,Description,Distance,Mass,StellarMagnitude,DiscoveryDate,ImagePath")] Planet planet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(planet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(planet);
        }

        // GET: Planets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planet = await _context.Planet.FindAsync(id);
            if (planet == null)
            {
                return NotFound();
            }
            return View(planet);
        }

        // POST: Planets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("planetId,Name,Description,Distance,Mass,StellarMagnitude,DiscoveryDate,ImagePath")] Planet planet)
        {
            if (id != planet.planetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(planet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanetExists(planet.planetId))
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
            return View(planet);
        }

        // GET: Planets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planet = await _context.Planet
                .FirstOrDefaultAsync(m => m.planetId == id);
            if (planet == null)
            {
                return NotFound();
            }

            return View(planet);
        }

        // POST: Planets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var planet = await _context.Planet.FindAsync(id);
            if (planet != null)
            {
                _context.Planet.Remove(planet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlanetExists(int id)
        {
            return _context.Planet.Any(e => e.planetId == id);
        }
    }
}
