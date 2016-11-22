using System.ComponentModel.DataAnnotations.Schema;

namespace Vehicles.Models
{
    [Table("NonMotorVehicles")]
    public abstract class NonMotorVehicle : Vehicle
    {
    }
}
