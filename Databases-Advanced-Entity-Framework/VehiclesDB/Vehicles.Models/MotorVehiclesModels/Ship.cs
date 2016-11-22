using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vehicles.Models.MotorVehiclesModels
{
    [Table("Ships")]
    public abstract class Ship : MotorVehicle
    {
        [Required]
        public string Nationality { get; set; }

        [Required]
        public string CaptainName { get; set; }

        public int SizeOfShipCrew { get; set; }
    }
}
