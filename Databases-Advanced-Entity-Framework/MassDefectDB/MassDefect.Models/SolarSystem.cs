using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MassDefect.Models
{
    public class SolarSystem
    {
        private ICollection<Planet> planets;
        private ICollection<Star> stars;

        public SolarSystem()
        {
            this.planets = new HashSet<Planet>();
            this.stars = new HashSet<Star>();
        }

        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        public virtual ICollection<Planet> Planets
        {
            get { return this.planets; }
            set { this.planets = value; }
        }

        public virtual ICollection<Star> Stars
        {
            get { return this.stars; }
            set { this.stars = value; }
        }
    }
}
