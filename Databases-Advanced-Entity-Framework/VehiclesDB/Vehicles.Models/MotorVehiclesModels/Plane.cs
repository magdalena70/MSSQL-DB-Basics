using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vehicles.Models.MotorVehiclesModels
{
    [Table("Planes")]
    public class Plane : MotorVehicle
    {
        [Required]
        public string AirlineOwner { get; set; }

        public string Color { get; set; }

        [Required]
        public int PassengersCapacity { get; set; }
    }
}
