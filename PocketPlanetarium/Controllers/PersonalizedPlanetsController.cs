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
    public class PersonalizedPlanetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonalizedPlanetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PersonalizedPlanets
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PersonalizedPlanet.Include(p => p.planet).Include(p => p.planetarium);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PersonalizedPlanets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalizedPlanet = await _context.PersonalizedPlanet
                .Include(p => p.planet)
                .Include(p => p.planetarium)
                .FirstOrDefaultAsync(m => m.personalizedPlanetId == id);
            if (personalizedPlanet == null)
            {
                return NotFound();
            }

            return View(personalizedPlanet);
        }

        // GET: PersonalizedPlanets/Create
        public IActionResult Create()
        {
            ViewData["planetId"] = new SelectList(_context.Planet, "planetId", "Description");
            ViewData["planetariumId"] = new SelectList(_context.Planetarium, "planetariumId", "userId");
            return View();
        }

        // POST: PersonalizedPlanets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("personalizedPlanetId,planetId,planetariumId")] PersonalizedPlanet personalizedPlanet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personalizedPlanet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["planetId"] = new SelectList(_context.Planet, "planetId", "Description", personalizedPlanet.planetId);
            ViewData["planetariumId"] = new SelectList(_context.Planetarium, "planetariumId", "userId", personalizedPlanet.planetariumId);
            return View(personalizedPlanet);
        }

        // GET: PersonalizedPlanets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalizedPlanet = await _context.PersonalizedPlanet.FindAsync(id);
            if (personalizedPlanet == null)
            {
                return NotFound();
            }
            ViewData["planetId"] = new SelectList(_context.Planet, "planetId", "Description", personalizedPlanet.planetId);
            ViewData["planetariumId"] = new SelectList(_context.Planetarium, "planetariumId", "userId", personalizedPlanet.planetariumId);
            return View(personalizedPlanet);
        }

        // POST: PersonalizedPlanets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("personalizedPlanetId,planetId,planetariumId")] PersonalizedPlanet personalizedPlanet)
        {
            if (id != personalizedPlanet.personalizedPlanetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personalizedPlanet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonalizedPlanetExists(personalizedPlanet.personalizedPlanetId))
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
            ViewData["planetId"] = new SelectList(_context.Planet, "planetId", "Description", personalizedPlanet.planetId);
            ViewData["planetariumId"] = new SelectList(_context.Planetarium, "planetariumId", "userId", personalizedPlanet.planetariumId);
            return View(personalizedPlanet);
        }

        // GET: PersonalizedPlanets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalizedPlanet = await _context.PersonalizedPlanet
                .Include(p => p.planet)
                .Include(p => p.planetarium)
                .FirstOrDefaultAsync(m => m.personalizedPlanetId == id);
            if (personalizedPlanet == null)
            {
                return NotFound();
            }

            return View(personalizedPlanet);
        }

        // POST: PersonalizedPlanets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var personalizedPlanet = await _context.PersonalizedPlanet.FindAsync(id);
            if (personalizedPlanet != null)
            {
                _context.PersonalizedPlanet.Remove(personalizedPlanet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonalizedPlanetExists(int id)
        {
            return _context.PersonalizedPlanet.Any(e => e.personalizedPlanetId == id);
        }
    }
}
