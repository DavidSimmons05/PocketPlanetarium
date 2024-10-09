using System.ComponentModel.DataAnnotations;

namespace PocketPlanetarium.Models
{
    public class Planet
    {
        public int planetId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public double Distance { get; set; }

        public double Mass { get; set; }

        [Required]
        public double StellarMagnitude { get; set; }

        [Required]
        public DateOnly DiscoveryDate { get; set; }

        public string ImagePath { get; set; }
    }
}
