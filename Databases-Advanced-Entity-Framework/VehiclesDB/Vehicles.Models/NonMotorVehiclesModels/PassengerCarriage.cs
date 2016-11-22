using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vehicles.Models.NonMotorVehiclesModels
{
    [Table("PassengerCarriages")]
    public class PassengerCarriage : Carriage
    {
        [Required]
        public int StandingPassengersCapacity { get; set; }
    }
}
