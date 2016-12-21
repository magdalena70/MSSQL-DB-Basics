using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeddingsPlanner.Models
{
    public class Wedding
    {
        public Wedding()
        {
            this.Venues = new HashSet<Venue>();
            this.Guests = new HashSet<Person>();
        }

        [Key]
        public int Id { get; set; }

        //[Required]
        public virtual Person Bride { get; set; }

        //[Required]
        public virtual Person Bridegroom { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public virtual Agency Agency { get; set; }

        public virtual ICollection<Venue> Venues { get; set; }

        public virtual ICollection<Person> Guests { get; set; }
    }
}
