using System.ComponentModel.DataAnnotations;

namespace HotelDB.Models
{
    public enum RoomStatuses
    {
        Free, Occupied, Booked
    }

    public partial class RoomStatus
    {
        [Key]
        public RoomStatuses Room_Status { get; set; }

        [MaxLength(1000)]
        public string Notes { get; set; }
    }
}
