namespace MassDefect.Data
{
    using Models;
    using System.Data.Entity;

    public class MassDefectContext : DbContext
    {
       
        public MassDefectContext()
            : base("name=MassDefectContext")
        {
        }

        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Anomalie> Anomalies { get; set; }
        public virtual DbSet<Planet> Planets { get; set; }
        public virtual DbSet<Star> Stars { get; set; }
        public virtual DbSet<SolarSystem> SolarSystems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Anomalie>()
                .HasMany(a => a.Victims)
                .WithMany(p => p.Anomalies)
                .Map(m => 
                {
                    m.MapLeftKey("AnomalyId");
                    m.MapRightKey("PersonId");
                    m.ToTable("AnomalyVictims");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}