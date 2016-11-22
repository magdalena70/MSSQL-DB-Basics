using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vehicles.Models.MotorVehiclesModels
{
    [Table("CargoShips")]
    public class CargoShip : Ship
    {
        [Required]
        public int MaxLoadKilograms { get; set; }
    }
}
