using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversitySystemDB.Models
{
    //[Table("Students")]
    public class Student : Person
    {
        private ICollection<Course> courses;

        public Student()
        {
            this.courses = new HashSet<Course>();
        }

        public decimal AverageGrade { get; set; }

        public string Attendance { get; set; }

        public virtual ICollection<Course> Courses
        {
            get { return this.courses; }
            set { this.courses = value; }
        }
    }
}
