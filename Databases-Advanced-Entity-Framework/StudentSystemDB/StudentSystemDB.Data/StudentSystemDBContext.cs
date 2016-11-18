namespace StudentSystemDB.Data
{
    using Models;
    using System.Data.Entity;

    public class StudentSystemDBContext : DbContext
    {
        public StudentSystemDBContext()
            : base("name=StudentSystemDBContext")
        {
        }

        public virtual IDbSet<Student> Students { get; set; }
        public virtual IDbSet<Course> Courses { get; set; }
        public virtual IDbSet<Resource> Resources { get; set; }
        public virtual IDbSet<Homework> Homeworks { get; set; }
        public virtual IDbSet<License> Licenses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasMany(st => st.Friends)
                .WithMany()
                .Map(m => 
                {
                    m.MapLeftKey("StudentId");
                    m.MapRightKey("FriendId");
                    m.ToTable("StudentsFriends");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}