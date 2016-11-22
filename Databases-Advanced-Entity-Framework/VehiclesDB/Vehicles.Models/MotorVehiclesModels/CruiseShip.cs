using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vehicles.Models.MotorVehiclesModels
{
    [Table("CruiseShips")]
    public class CruiseShip : Ship
    {
        [Required]
        public int PassengersCapacity { get; set; }
    }
}
