using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PocketPlanetarium.Models
{
    public class Planetarium
    {
        public int planetariumId { get; set; }

        //FK's
        [Required]
        public string userId { get; set; }
        public IdentityUser user { get; set; }
    }
}
