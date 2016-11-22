using System.ComponentModel.DataAnnotations.Schema;
using Vehicles.Models.MotorVehiclesModels;

namespace Vehicles.Models.NonMotorVehiclesModels
{
    [Table("Carriages")]
    public abstract class Carriage : NonMotorVehicle
    {
        public int PassengersSeatsCapacity { get; set; }

        public virtual Train Train { get; set; }
    }
}
