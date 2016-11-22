using System.ComponentModel.DataAnnotations.Schema;

namespace Vehicles.Models.NonMotorVehiclesModels
{
    [Table("Bikes")]
    public class Bike : NonMotorVehicle
    {
        public int ShiftsCount { get; set; }

        public string Color { get; set; }
    }
}
