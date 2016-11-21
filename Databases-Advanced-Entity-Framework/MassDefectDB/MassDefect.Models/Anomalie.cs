
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MassDefect.Models
{
    public class Anomalie
    {
        private ICollection<Person> victims;

        public Anomalie()
        {
            this.victims = new HashSet<Person>();
        }

        [Key]
        public int Id { get; set; }

        public virtual Planet OriginPlanet { get; set; }

        public virtual Planet TeleportPlanet { get; set; }

        public virtual ICollection<Person> Victims
        {
            get { return this.victims; }
            set { this.victims = value; }
        }
    }
}
