using System.ComponentModel.DataAnnotations;

namespace HotelDB.Models
{
    public partial class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(30)]
        public string FirstName { get; set; }

        [Required, MaxLength(30)]
        public string LastName { get; set; }

        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Notes { get; set; }
    }
}
