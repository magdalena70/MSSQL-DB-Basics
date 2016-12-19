using PhotographyWorkshops.Models.attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotographyWorkshops.Models
{
    public class Camera
    {
        public Camera()
        {
            this.PrimaryCameraOwners = new HashSet<Photographer>();
            this.SecondaryCameraOwners = new HashSet<Photographer>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        public bool IsFullFrame { get; set; }

        [Required, MinISO_Validation]
        public int MinISO { get; set; }

        public int MaxISO { get; set; }

        public virtual ICollection<Photographer> PrimaryCameraOwners { get; set; }

        public virtual ICollection<Photographer> SecondaryCameraOwners { get; set; }
    }
}
