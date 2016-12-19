using System.ComponentModel.DataAnnotations;

namespace PhotographyWorkshops.Models
{
    public class Accessory
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual Photographer Owner { get; set; }
    }
}
