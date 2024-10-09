using System.ComponentModel.DataAnnotations;

namespace PocketPlanetarium.Models
{
    public class Star
    {
        public int starId { get; set; }

        [Required]
        public string Name { get; set; }

        public double distance { get; set; }

        public double mass { get; set; }

        [Required]
        public double StellarMagnitude { get; set; }

        [Required]
        public DateOnly DiscoveryDate { get; set; }

        public string ImagePath { get; set; }
    }
}
