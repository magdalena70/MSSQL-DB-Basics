using System.ComponentModel.DataAnnotations.Schema;

namespace Vehicles.Models.MotorVehiclesModels
{
    [Table("Cars")]
    public class Car : MotorVehicle
    {
        public int NumberOfDoors { get; set; }

        public string InformationInsurance { get; set; }
    }
}
