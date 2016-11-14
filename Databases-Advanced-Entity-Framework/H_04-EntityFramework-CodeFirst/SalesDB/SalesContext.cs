namespace SalesDB
{
    using Models;
    using System.Data.Entity;

    public class SalesContext : DbContext
    {
        public SalesContext()
            : base("name=SalesContext")
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<SalesContext>());
            Database.Initialize(true);
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<StoreLocation> StoreLocations{ get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
    }

}