using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarDealerDB.Models
{
    public class Part
    {
        private ICollection<Car> cars;
        public Part()
        {
            this.cars = new HashSet<Car>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public virtual Supplier Supplier { get; set; }

        public virtual ICollection<Car> Cars
        {
            get { return this.cars; }
            set { this.cars = value; }
        }
    }
}
