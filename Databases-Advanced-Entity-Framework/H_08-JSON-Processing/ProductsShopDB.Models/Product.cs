using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsShopDB.Models
{
    public class Product
    {
        private ICollection<Categorie> categories;
        public Product()
        {
            this.categories = new HashSet<Categorie>();
        }

        [Key]
        public int Id { get; set; }

        [Required, MinLength(3)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public User Buyer { get; set; }

        [Required]
        public User Seller { get; set; }

        public ICollection<Categorie> Categories
        {
            get { return this.categories; }
            set { this.categories = value; }
        }
    }
}
