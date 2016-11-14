using GringottsDB.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace GringottsDB.Models
{
    public partial class WizardDeposit
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(60)]
        [Required]
        public string LastName { get; set; }

        [MaxLength(1000)]
        public string Notes { get; set; }

        [Required]
        [AgeValidation]
        public int Age { get; set; }

        public  MagicWand MagicWand { get; set; }

        public Deposit Deposit { get; set; }
    }
}
