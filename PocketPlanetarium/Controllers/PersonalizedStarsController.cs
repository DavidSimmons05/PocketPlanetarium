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
    public class PersonalizedStarsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonalizedStarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PersonalizedStars
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PersonalizedStar.Include(p => p.planetarium).Include(p => p.star);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PersonalizedStars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalizedStar = await _context.PersonalizedStar
                .Include(p => p.planetarium)
                .Include(p => p.star)
                .FirstOrDefaultAsync(m => m.personalizedStarId == id);
            if (personalizedStar == null)
            {
                return NotFound();
            }

            return View(personalizedStar);
        }

        // GET: PersonalizedStars/Create
        public IActionResult Create()
        {
            ViewData["planetariumId"] = new SelectList(_context.Planetarium, "planetariumId", "userId");
            ViewData["starId"] = new SelectList(_context.Star, "starId", "Name");
            return View();
        }

        // POST: PersonalizedStars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("personalizedStarId,starId,planetariumId")] PersonalizedStar personalizedStar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personalizedStar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["planetariumId"] = new SelectList(_context.Planetarium, "planetariumId", "userId", personalizedStar.planetariumId);
            ViewData["starId"] = new SelectList(_context.Star, "starId", "Name", personalizedStar.starId);
            return View(personalizedStar);
        }

        // GET: PersonalizedStars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalizedStar = await _context.PersonalizedStar.FindAsync(id);
            if (personalizedStar == null)
            {
                return NotFound();
            }
            ViewData["planetariumId"] = new SelectList(_context.Planetarium, "planetariumId", "userId", personalizedStar.planetariumId);
            ViewData["starId"] = new SelectList(_context.Star, "starId", "Name", personalizedStar.starId);
            return View(personalizedStar);
        }

        // POST: PersonalizedStars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("personalizedStarId,starId,planetariumId")] PersonalizedStar personalizedStar)
        {
            if (id != personalizedStar.personalizedStarId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personalizedStar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonalizedStarExists(personalizedStar.personalizedStarId))
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
            ViewData["planetariumId"] = new SelectList(_context.Planetarium, "planetariumId", "userId", personalizedStar.planetariumId);
            ViewData["starId"] = new SelectList(_context.Star, "starId", "Name", personalizedStar.starId);
            return View(personalizedStar);
        }

        // GET: PersonalizedStars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalizedStar = await _context.PersonalizedStar
                .Include(p => p.planetarium)
                .Include(p => p.star)
                .FirstOrDefaultAsync(m => m.personalizedStarId == id);
            if (personalizedStar == null)
            {
                return NotFound();
            }

            return View(personalizedStar);
        }

        // POST: PersonalizedStars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var personalizedStar = await _context.PersonalizedStar.FindAsync(id);
            if (personalizedStar != null)
            {
                _context.PersonalizedStar.Remove(personalizedStar);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonalizedStarExists(int id)
        {
            return _context.PersonalizedStar.Any(e => e.personalizedStarId == id);
        }
    }
}
