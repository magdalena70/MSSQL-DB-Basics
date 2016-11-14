using System.ComponentModel.DataAnnotations;

namespace HotelDB.Models
{
    public partial class Customer
    {
        [Key]
        public int AccountNumber { get; set; }

        [Required, MaxLength(30)]
        public string FirstName { get; set; }

        [Required, MaxLength(30)]
        public string LastName { get; set; }

        [MaxLength(50)]
        public string PhoneNumber { get; set; }

        [MaxLength(50)]
        public string EmergencyName { get; set; }

        [MaxLength(30)]
        public string EmergencyNumber { get; set; }

        [MaxLength(1000)]
        public string Notes { get; set; }
    }
}
