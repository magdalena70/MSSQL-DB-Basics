using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeddingsPlanner.Models
{
    public class Venue
    {
        public Venue()
        {
            this.WeddingCelebrations = new HashSet<Wedding>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Capacity { get; set; }

        public string Town { get; set; }

        public virtual ICollection<Wedding> WeddingCelebrations { get; set; }
    }
}
