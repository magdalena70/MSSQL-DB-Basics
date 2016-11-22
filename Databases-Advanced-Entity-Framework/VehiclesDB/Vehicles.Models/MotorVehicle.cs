using System.ComponentModel.DataAnnotations.Schema;

namespace Vehicles.Models
{
    [Table("MotorVehicles")]
    public abstract class MotorVehicle : Vehicle
    {
        public int NumberOfEngines { get; set; }

        public string EngineType { get; set; }

        public int TankCapacity { get; set; }
    }
}
