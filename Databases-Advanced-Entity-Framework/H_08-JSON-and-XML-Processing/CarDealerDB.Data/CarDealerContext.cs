namespace CarDealerDB.Data
{
    using Models;
    using System.Data.Entity;

    public class CarDealerContext : DbContext
    {
        
        public CarDealerContext()
            : base("name=CarDealerContext")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Part> Parts { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
    }
}