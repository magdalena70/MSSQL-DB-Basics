using System;
using System.ComponentModel.DataAnnotations;
using H_04_EntityFramework_CodeFirst.Attributes;

namespace H_04_EntityFramework_CodeFirst.Models
{
    public partial class User
    {
        [Key]
        public int Id { get; set; }

        [MinLength(4), MaxLength(30)]
        [Required]
        public string Username { get; set; }

        [MaxLength(30)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(30)]
        [Required]
        public string LastName { get; set; }

        [MinLength(6), MaxLength(50)]
        [Required]
        [PassValidation]
        public string Password { get; set; }

        [RegularExpression(@"^([0-9]*[a-z][\w\.\-]*[0-9]*[a-z]+)@([\w\-]+)((\.(\w)+)+)([a-zA-Z]{2,4}|[0-9]{1,3})$")]
        public string Email { get; set; }

        [MaxLength(1024*1024)]
        public byte[] ProfilePicture { get; set; }

        public DateTime RegisteredOn { get; set; }

        public DateTime LastTimeLoggedIn { get; set; }
        
        [Range(1, 120)]
        public int Age { get; set; }

        public bool IsDeleted { get; set; }

        public Town bornTown { get; set; }

        public Town currentlyLivingTown { get; set; }
    }
}
