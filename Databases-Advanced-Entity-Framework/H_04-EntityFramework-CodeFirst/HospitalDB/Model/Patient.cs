using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HospitalDB.Model
{
    public class Patient
    {
        private ICollection<Visitation> visitations;
        private ICollection<Diagnose> diagnoses;
        private ICollection<Medicament> medicaments;

        public Patient()
        {
            this.visitations = new HashSet<Visitation>();
            this.diagnoses = new HashSet<Diagnose>();
            this.medicaments = new HashSet<Medicament>();
        }

        [Key]
        public int Id { get; set; }

        [Required, MaxLength(30)]
        public string FirstName { get; set; }

        [Required, MaxLength(30)]
        public string LastName { get; set; }

        [Required, MaxLength(100)]
        public string Address { get; set; }

        [RegularExpression(@"^([0-9]*[a-z][\w\.\-]*[0-9]*[a-z]+)@([\w\-]+)(\.+[a-zA-Z]*\.*[a-zA-Z]{1,3})$")]
        public string Email { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [MaxLength(1024 * 1024)]
        public byte[] Picture { get; set; }

        public bool HasMedicalInsurance { get; set; }

        public virtual ICollection<Visitation> Visitations
        {
            get { return this.visitations; }
            set { this.visitations = value; }
        }

        public virtual ICollection<Diagnose> Diagnoses
        {
            get { return this.diagnoses; }
            set { this.diagnoses = value; }
        }

        public virtual ICollection<Medicament> Medicaments
        {
            get { return this.medicaments; }
            set { this.medicaments = value; }
        }
    }
}
