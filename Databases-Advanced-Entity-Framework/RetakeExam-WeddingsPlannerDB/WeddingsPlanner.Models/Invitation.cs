using System.ComponentModel.DataAnnotations;

namespace WeddingsPlanner.Models
{
    public class Invitation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public virtual Wedding Wedding { get; set; }

        [Required]
        public virtual Person Guest { get; set; }

        public virtual Gift GiftPresent { get; set; }

        public virtual Cash CashPresent { get; set; }

        public bool Attending { get; set; }

        [Required]
        public string Family { get; set; }
    }
}
