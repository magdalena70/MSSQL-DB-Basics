using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentSystemDB.Models
{
    public enum TypeOfResource
    {
        Video, Presentation, Document, Other
    }

    public class Resource
    {
        private ICollection<License> licenses;

        public Resource()
        {
            this.licenses = new HashSet<License>();
        }

        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public TypeOfResource Type { get; set; }

        [Required, MaxLength(100)]
        public string URL { get; set; }

        public Course Course { get; set; }

        public virtual ICollection<License> Licenses
        {
            get { return this.licenses; }
            set { this.licenses = value; }
        }
    }
}
