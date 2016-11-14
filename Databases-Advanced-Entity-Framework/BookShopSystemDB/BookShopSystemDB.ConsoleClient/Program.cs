using BookShopSystemDB.Data;
using System.Linq;

namespace BookShopSystemDB.ConsoleClient
{
    class Program
    {
        static void Main()
        {
            var context = new BookShopContext();
            var count = context.Books.Count();
        }
    }
}
