namespace UniversitySystemDB
{
    using Models;
    using System.Data.Entity;

    public class TphUniversityContext : DbContext
    {
        public TphUniversityContext()
            : base("name=TphUniversityContext")
        {
        }

        public virtual IDbSet<Person> People { get; set; }
        public virtual IDbSet<Course> Courses { get; set; }
    }
}