using System.ComponentModel.DataAnnotations;

namespace HospitalDB.Model
{
    public class Diagnose
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        public string Comments { get; set; }

        [Required]
        public Patient Patient { get; set; }
    }
}
