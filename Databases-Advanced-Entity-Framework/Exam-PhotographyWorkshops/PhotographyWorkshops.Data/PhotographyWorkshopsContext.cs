namespace PhotographyWorkshops.Data
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class PhotographyWorkshopsContext : DbContext
    {
        public PhotographyWorkshopsContext()
            : base("name=PhotographyWorkshopsContext")
        {
           
        }

        public virtual DbSet<Lens> Lenses { get; set; }
        public virtual DbSet<Accessory> Accessories { get; set; }
        public virtual DbSet<Camera> Cameras { get; set; }
        public virtual DbSet<Workshop> Workshops { get; set; }
        public virtual DbSet<Photographer> Photographers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Photographer>()
               .HasRequired(a => a.PrimaryCamera)
               .WithMany(c => c.PrimaryCameraOwners)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<Photographer>()
               .HasRequired(a => a.SecondaryCamera)
               .WithMany(c => c.SecondaryCameraOwners)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<Photographer>()
                .HasMany(p => p.WorkshopsParticipate)
                .WithMany(w => w.Participants)
                .Map(entity =>
                {
                    entity.ToTable("WorkshopParticipants");
                    entity.MapLeftKey("WorkshopId");
                    entity.MapRightKey("ParicipantId");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}