using System.Data.Entity;
using UniversitySystemDB.Models;

namespace UniversitySystemDB
{
    public class TptUniversityContext : DbContext
    {
        public TptUniversityContext()
            : base("TptUniversityContext")
        {
        }

        public virtual IDbSet<Person> People { get; set; }
        public virtual IDbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().ToTable("Students");
            modelBuilder.Entity<Teacher>().ToTable("Teachers");

            base.OnModelCreating(modelBuilder);
        }
    }
}
