namespace ProductsShopDB.Data
{
    using Models;
    using System.Data.Entity;

    public class ProductsShopContext : DbContext
    {
        
        public ProductsShopContext()
            : base("name=ProductsShopContext")
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Categorie> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Friends)
                .WithMany()
                .Map(m => 
                {
                    m.MapLeftKey("UserId");
                    m.MapRightKey("FriendId");
                    m.ToTable("UserFriends");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}