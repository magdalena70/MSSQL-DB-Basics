using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HospitalDB.Model
{
    public class Doctor
    {
        private ICollection<Visitation> visitations;

        public Doctor()
        {
            this.visitations = new HashSet<Visitation>();
        }

        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(100)]
        public string Specialty { get; set; }

        public virtual ICollection<Visitation> Visitations
        {
            get { return this.visitations; }
            set { this.visitations = value; }
        }
    }
}
