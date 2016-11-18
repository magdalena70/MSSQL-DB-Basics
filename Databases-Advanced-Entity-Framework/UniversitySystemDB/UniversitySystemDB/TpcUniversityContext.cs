using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using UniversitySystemDB.Models;

namespace UniversitySystemDB
{
    public class TpcUniversityContext : DbContext
    {
        public TpcUniversityContext()
            :base("TpcUniversityContext")
        {
        }

        public virtual IDbSet<Person> People { get; set; }
        public virtual IDbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            modelBuilder.Entity<Student>().Map(m =>
                {
                    m.MapInheritedProperties();
                    m.ToTable("Students");
                }
            );

            modelBuilder.Entity<Teacher>().Map(m => 
                {
                    m.MapInheritedProperties();
                    m.ToTable("Teachers");
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
