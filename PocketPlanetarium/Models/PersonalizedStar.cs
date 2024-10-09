using System.ComponentModel.DataAnnotations;

namespace PocketPlanetarium.Models
{
    public class PersonalizedStar
    {
        public int personalizedStarId { get; set; }

        //FK
        [Required]
        public int starId { get; set; }
        public Star star { get; set; }

        [Required]
        public int planetariumId { get; set; }
        public Planetarium planetarium { get; set; }
    }
}
