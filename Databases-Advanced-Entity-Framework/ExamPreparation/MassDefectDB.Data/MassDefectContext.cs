namespace MassDefectDB.Data
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
        public virtual DbSet<Planet> Planets { get; set; }
        public virtual DbSet<Star> Stars { get; set; }
        public virtual DbSet<SolarSystem> SolarSystems { get; set; }
        public virtual DbSet<Anomaly> Anomalies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Anomaly>()
                .HasOptional(a => a.OriginPlanet)
                .WithMany(p => p.OriginAnomalies)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Anomaly>()
                .HasOptional(a => a.TeleportPlanet)
                .WithMany(p => p.TeleportAnomalies)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Anomaly>()
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