using System;
using System.ComponentModel.DataAnnotations;

namespace HotelDB.Models
{
    public partial class Occupancies
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime DateOccupied { get; set; }

        [Range(0, int.MaxValue)]
        public int RoomNumber { get; set; }

        [Range(0, int.MaxValue)]
        public int AccountNumber { get; set; }

        [Range(typeof(decimal), "0", "10000000000")]
        public decimal RateApplied { get; set; }

        [Range(typeof(decimal), "0", "10000000000")]
        public decimal PhoneCharge { get; set; }

        [MaxLength(1000)]
        public string Notes { get; set; }
    }
}
