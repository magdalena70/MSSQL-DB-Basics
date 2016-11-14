using System.ComponentModel.DataAnnotations;

namespace HotelDB.Models
{
    public enum BedTypes
    {
        Single_Bed, Double_Bed, KingSize
    }

    public partial class BedType
    {
        [Key]
        public BedTypes Type { get; set; }

        [MaxLength(1000)]
        public string Notes { get; set; }
    }
}
