using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingsPlanner.Models
{
    public enum GiftSize
    {
        Small, Medium, Large, NotSpecified
    }

    public class Gift
    {
        [Key]
        [ForeignKey("Invitation")]
        public int Id { get; set; }

        public virtual Person Owner { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual GiftSize Size { get; set; }

        public virtual Invitation Invitation { get; set; }
    }
}
