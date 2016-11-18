using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversitySystemDB.Models
{
    //[Table("Teachers")]
    public class Teacher : Person
    {
        private ICollection<Course> courses;

        public Teacher()
        {
            this.courses = new HashSet<Course>();
        }

        [Required]
        public string Email { get; set; }

        public decimal SalaryPerHour { get; set; }

        public virtual ICollection<Course> Courses
        {
            get { return this.courses; }
            set { this.courses = value; }
        }
    }
}
