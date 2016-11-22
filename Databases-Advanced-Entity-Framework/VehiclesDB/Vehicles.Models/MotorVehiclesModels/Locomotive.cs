using System.ComponentModel.DataAnnotations.Schema;

namespace Vehicles.Models.MotorVehiclesModels
{
    [Table("Locomotives")]
    public class Locomotive : MotorVehicle
    {
        public string LocomotiveModel { get; set; }

        public int Power { get; set; }

        public virtual Train Train { get; set; }
    }
}
