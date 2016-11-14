using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalDB.Model
{
    public class Visitation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Comments { get; set; }

        [Required]
        public Patient Patient { get; set; }

        [Required]
        public Doctor Doctor { get; set; }
    }
}
