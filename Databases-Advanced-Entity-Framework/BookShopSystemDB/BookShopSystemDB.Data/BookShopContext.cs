namespace BookShopSystemDB.Data
{
    using Models;
    using System.Data.Entity;

    public class BookShopContext : DbContext
    {
        public BookShopContext()
            : base("name=BookShopContext")
        {
        }

        public virtual IDbSet<Book> Books { get; set; }
        public virtual IDbSet<Author> Authors { get; set; }
        public virtual IDbSet<Category> Categories { get; set; }
       
    }
}