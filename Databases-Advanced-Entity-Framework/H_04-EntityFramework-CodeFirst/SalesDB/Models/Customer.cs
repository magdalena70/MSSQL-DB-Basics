using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalesDB.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [MinLength(6), MaxLength(30)]
        public string Email { get; set; }

        [MaxLength(50)]
        public string CreditCardNumber { get; set; }

        public ICollection<Sale> SalesForCustomer { get; set; }
    }
}
