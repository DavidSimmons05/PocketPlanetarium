using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PocketPlanetarium.Models;

namespace PocketPlanetarium.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<PocketPlanetarium.Models.Planet> Planet { get; set; } = default!;
        public DbSet<PocketPlanetarium.Models.Star> Star { get; set; } = default!;
        public DbSet<PocketPlanetarium.Models.Planetarium> Planetarium { get; set; } = default!;
        public DbSet<PocketPlanetarium.Models.PersonalizedPlanet> PersonalizedPlanet { get; set; } = default!;
        public DbSet<PocketPlanetarium.Models.PersonalizedStar> PersonalizedStar { get; set; } = default!;
    }
}
