using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MassDefectDB.Models
{
    public class Planet
    {
        public Planet()
        {
            this.People = new HashSet<Person>();
            this.OriginAnomalies = new HashSet<Anomaly>();
            this.TeleportAnomalies = new HashSet<Anomaly>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("Sun")]
        public int? StarId { get; set; }

        public virtual Star Sun { get; set; }

        [ForeignKey("SolarSystem")]
        public int? SolarSystemId { get; set; }

        public virtual SolarSystem SolarSystem { get; set; }

        public virtual ICollection<Person> People { get; set; }

        public virtual ICollection<Anomaly> OriginAnomalies { get; set; }

        public virtual ICollection<Anomaly> TeleportAnomalies { get; set; }
    }
}
