using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vehicles.Models.NonMotorVehiclesModels
{
    [Table("SleepingCarriages")]
    public class SleepingCarriage : Carriage
    {
        [Required]
        public int BedsCount { get; set; }
    }
}
