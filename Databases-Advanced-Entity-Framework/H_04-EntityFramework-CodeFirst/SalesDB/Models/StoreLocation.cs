using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalesDB.Models
{
    public class StoreLocation
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string LocationName { get; set; }

        public ICollection<Sale> SalesInStore { get; set; }
    }
}
