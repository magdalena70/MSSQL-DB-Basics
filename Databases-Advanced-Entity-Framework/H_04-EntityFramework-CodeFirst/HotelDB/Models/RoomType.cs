using System.ComponentModel.DataAnnotations;

namespace HotelDB.Models
{
    public enum RoomTypes
    {
        Single_Room, Double_Room, Family_Room, Apartment
    }

    public partial class RoomType
    {
        [Key]
        public RoomTypes Type { get; set; }

        [MaxLength(1000)]
        public string Notes { get; set; }
    }
}
