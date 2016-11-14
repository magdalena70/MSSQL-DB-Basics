using System.ComponentModel.DataAnnotations;

namespace HospitalDB.Model
{
    public class Medicament
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public Patient Patient { get; set; }
    }
}
