using PhotographyWorkshops.Models.attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotographyWorkshops.Models
{
    public class Photographer
    {
        public Photographer()
        {
            this.Lenses = new HashSet<Lens>();
            this.Accessories = new HashSet<Accessory>();
            this.WorkshopsParticipate = new HashSet<Workshop>();
            this.WorkshopsTrainer = new HashSet<Workshop>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required, MinLength(2), MaxLength(50)]
        public string LastName { get; set; }

        [PhoneValidation]
        public string Phone { get; set; }

        //public int? PrimaryCameraId { get; set; }
        //public int? SecondaryCameraId { get; set; }

        [Required]
        //[ForeignKey("PrimaryCameraId")]
        public virtual Camera PrimaryCamera { get; set; }

        [Required]
        //[ForeignKey("SecondaryCameraId")]
        public virtual Camera SecondaryCamera  { get; set; }

        public virtual ICollection<Lens> Lenses { get; set; }

        public virtual ICollection<Accessory> Accessories { get; set; }

        public virtual ICollection<Workshop> WorkshopsParticipate { get; set; }

        [InverseProperty("Trainer")]
        public virtual ICollection<Workshop> WorkshopsTrainer { get; set; }
    }
}
