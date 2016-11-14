namespace H_04_EntityFramework_CodeFirst
{
    using Models;
    using System.Data.Entity;

    public class UserContext : DbContext
    {
       
        public UserContext()
            : base("name=UserContext")
        {
        }

         public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Town> Towns { get; set; }
    }
}