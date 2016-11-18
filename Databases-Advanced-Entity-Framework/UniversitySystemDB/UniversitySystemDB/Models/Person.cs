using System.ComponentModel.DataAnnotations;

namespace UniversitySystemDB.Models
{
    public abstract class Person
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(30)]
        public string FirstName { get; set; }

        [Required, MaxLength(30)]
        public string LastName { get; set; }

        [MaxLength(30)]
        public string PhoneNumber { get; set; }
    }
}
