using System.ComponentModel.DataAnnotations;

namespace HotelDB.Models
{
    public partial class Room
    {
        [Key]
        public int RoomNumber { get; set; }

        [Range(typeof(decimal), "0", "1000")]
        public decimal Rate { get; set; }

        [MaxLength(1000)]
        public string Notes { get; set; }

        [Required]
        public RoomType RoomType { get; set; }

        [Required]
        public BedType BedType { get; set; }

        [Required]
        public RoomStatus RoomStatus { get; set; }
    }
}
