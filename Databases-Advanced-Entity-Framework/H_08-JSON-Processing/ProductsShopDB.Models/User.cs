using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsShopDB.Models
{
    public class User
    {
        private ICollection<User> friends;
        private ICollection<Product> boughtProducts;
        private ICollection<Product> soldProducts;

        public User()
        {
            this.friends = new HashSet<User>();
            this.boughtProducts = new HashSet<Product>();
            this.soldProducts = new HashSet<Product>();
        }

        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        [Required, MinLength(3)]
        public string LastName { get; set; }

        public int Age { get; set; }

        public ICollection<User> Friends
        {
            get { return this.friends; }
            set { this.friends = value; }
        }

        [InverseProperty("Buyer")]
        public ICollection<Product> BoughtProducts
        {
            get { return this.boughtProducts; }
            set { this.boughtProducts = value; }
        }

        [InverseProperty("Seller")]
        public ICollection<Product> SoldProducts
        {
            get { return this.soldProducts; }
            set { this.soldProducts = value; }
        }
    }
}
