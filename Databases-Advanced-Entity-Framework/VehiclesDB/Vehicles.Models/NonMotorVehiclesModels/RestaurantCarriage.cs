using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vehicles.Models.NonMotorVehiclesModels
{
    [Table("RestaurantCarriage")]
    public class RestaurantCarriage : Carriage
    {
        [Required]
        public int TablesCount { get; set; }
    }
}
