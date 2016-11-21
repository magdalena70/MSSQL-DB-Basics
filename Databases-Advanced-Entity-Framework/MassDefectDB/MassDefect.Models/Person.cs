using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MassDefect.Models
{
    public class Person
    {
        private ICollection<Anomalie> anomalies;

        public Person()
        {
            this.anomalies = new HashSet<Anomalie>();
        }

        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        public virtual Planet HomePlanet { get; set; }

        public virtual ICollection<Anomalie> Anomalies
        {
            get { return this.anomalies; }
            set { this.anomalies = value; }
        }
    }
}
