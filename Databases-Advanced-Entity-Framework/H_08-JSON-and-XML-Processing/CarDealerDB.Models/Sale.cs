using System.ComponentModel.DataAnnotations;

namespace CarDealerDB.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Discount { get; set; }

        public virtual Car Car { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
