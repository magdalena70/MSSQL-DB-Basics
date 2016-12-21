using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WeddingsPlanner.Models.attributes;

namespace WeddingsPlanner.Models
{
    public enum Gender
    {
        Male, Female, NotSpecified
    }

    public class Person
    {
        [Key]
        public int Id { get; set; }

        [Required, MinLength(1), MaxLength(60)]
        public string FirstName { get; set; }

        [Required, MinLength(1), MaxLength(1)]
        public string MiddleNameInitial { get; set; }

        [Required, MinLength(2)]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName
        {
            get { return this.FirstName + " " + this.MiddleNameInitial + " " + this.LastName; }
        }

        [Required]
        public virtual Gender Gender { get; set; }

        public DateTime? Birthdate { get; set; }

        [NotMapped]
        public int Age { get; set; }

        public string Phone { get; set; }

        [EmailValidation]
        public string Email { get; set; }
    }
}
