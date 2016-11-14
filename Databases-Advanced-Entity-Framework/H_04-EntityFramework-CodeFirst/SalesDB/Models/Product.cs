using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalesDB.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Range(0, double.MaxValue)]
        public double Quantity { get; set; }

        [Range(typeof(decimal), "0", "10000000000")]
        public decimal Price { get; set; }

        public ICollection<Sale> SalesOfProduct { get; set; }
    }
}
