using System.ComponentModel.DataAnnotations;

namespace StudentSystemDB.Models
{
    public class License
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        public Resource Resource { get; set; }
    }
}
