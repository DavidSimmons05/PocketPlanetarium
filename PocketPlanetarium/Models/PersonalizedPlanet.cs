using System.ComponentModel.DataAnnotations;

namespace PocketPlanetarium.Models
{
    public class PersonalizedPlanet
    {
        public int personalizedPlanetId { get; set; }

        //FK
        [Required]
        public int planetId { get; set; }
        public Planet planet { get; set; }

        [Required]
        public int planetariumId { get; set; }
        public Planetarium planetarium { get; set; }
    }
}
