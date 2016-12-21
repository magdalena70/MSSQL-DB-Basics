using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingsPlanner.Models
{
    public class Cash
    {
        [Key]
        [ForeignKey("Invitation")]
        public int Id { get; set; }

        public virtual Person Owner { get; set; }

        [Required]
        public decimal CashAmount { get; set; }

        public virtual Invitation Invitation { get; set; }
    }
}
