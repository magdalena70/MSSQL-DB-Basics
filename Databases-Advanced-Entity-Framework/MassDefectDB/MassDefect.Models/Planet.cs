using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MassDefect.Models
{
    public class Planet
    {
        private ICollection<Anomalie> originAnomalies;
        private ICollection<Anomalie> teleportAnomalies;
        private ICollection<Person> people;

        public Planet()
        {
            this.originAnomalies = new HashSet<Anomalie>();
            this.teleportAnomalies = new HashSet<Anomalie>();
            this.people = new HashSet<Person>();
        }

        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        public virtual SolarSystem SolarSystem { get; set; }

        public virtual Star Sun { get; set; }

        [InverseProperty("OriginPlanet")]
        public virtual ICollection<Anomalie> OriginAnomalies
        {
            get { return this.originAnomalies; }
            set { this.originAnomalies = value; }
        }

        [InverseProperty("TeleportPlanet")]
        public virtual ICollection<Anomalie> TeleportAnomalies
        {
            get { return this.teleportAnomalies; }
            set { this.teleportAnomalies = value; }
        }

        public virtual ICollection<Person> People
        {
            get { return this.people; }
            set { this.people = value; }
        }
    }
}
