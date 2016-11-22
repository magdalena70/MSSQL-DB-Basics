using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vehicles.Models
{
    [Table("Vehicles")]
    public abstract class Vehicle
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Manufacturer { get; set; }

        [Required, MaxLength(50)]
        public string Model { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int MaxSpeed { get; set; }
    }
}
