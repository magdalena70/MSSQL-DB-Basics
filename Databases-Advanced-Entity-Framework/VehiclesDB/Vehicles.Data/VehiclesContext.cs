namespace Vehicles.Data
{
    using Models;
    using Models.MotorVehiclesModels;
    using System.Data.Entity;

    public class VehiclesContext : DbContext
    {
        
        public VehiclesContext()
            : base("name=VehiclesContext")
        {
        }

        public virtual DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Train>()
                .HasOptional(t => t.Locomotive)
                .WithRequired(l => l.Train);

            base.OnModelCreating(modelBuilder);
        }
    }
}