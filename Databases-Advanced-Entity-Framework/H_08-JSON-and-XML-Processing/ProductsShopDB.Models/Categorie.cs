using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductsShopDB.Models
{
    public class Categorie
    {
        private ICollection<Product> products;
        public Categorie()
        {
            this.products = new HashSet<Product>();
        }

        [Key]
        public int Id { get; set; }

        [Required, MinLength(3), MaxLength(15)]
        public string Name { get; set; }

        public ICollection<Product> Products
        {
            get { return this.products; }
            set { this.products = value; }
        }
    }
}
