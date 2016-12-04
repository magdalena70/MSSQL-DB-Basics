using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarDealerDB.Models
{
    public class Car
    {
        private ICollection<Sale> sales;
        private ICollection<Part> parts;

        public Car()
        {
            this.sales = new HashSet<Sale>();
            this.parts = new HashSet<Part>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        public long TravelledDistance { get; set; }

        public virtual ICollection<Part> Parts
        {
            get { return this.parts; }
            set { this.parts = value; }
        }

        public virtual ICollection<Sale> Sales
        {
            get { return this.sales; }
            set { this.sales = value; }
        }
    }
}
