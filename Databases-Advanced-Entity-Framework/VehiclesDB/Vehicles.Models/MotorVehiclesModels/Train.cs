using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Vehicles.Models.NonMotorVehiclesModels;

namespace Vehicles.Models.MotorVehiclesModels
{
    [Table("Trains")]
    public class Train : MotorVehicle
    {
        private ICollection<Carriage> carriages;

        public Train()
        {
            this.carriages = new HashSet<Carriage>();
        }

        public virtual Locomotive Locomotive { get; set; }

        public int numberOfCarriages { get; set; }

        public virtual ICollection<Carriage> Carriages
        {
            get { return this.carriages; }
            set { this.carriages = value; }
        }
    }
}
