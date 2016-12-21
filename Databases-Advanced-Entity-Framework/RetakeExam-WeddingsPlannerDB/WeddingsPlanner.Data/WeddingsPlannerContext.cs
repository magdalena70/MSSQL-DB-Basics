namespace WeddingsPlanner.Data
{
    using Models;
    using System.Data.Entity;

    public class WeddingsPlannerContext : DbContext
    {
        public WeddingsPlannerContext()
            : base("name=WeddingsPlannerContext")
        {
        }

        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Agency> Agencies { get; set; }
        public virtual DbSet<Venue> Venues { get; set; }
        public virtual DbSet<Wedding> Weddings { get; set; }
        public virtual DbSet<Invitation> Invitations { get; set; }
        public virtual DbSet<Gift> Gifts { get; set; }
        public virtual DbSet<Cash> Cash { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}