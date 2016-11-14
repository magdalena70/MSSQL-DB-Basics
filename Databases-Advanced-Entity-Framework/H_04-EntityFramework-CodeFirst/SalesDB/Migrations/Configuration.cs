namespace SalesDB.Migrations
{
    using Models;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SalesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SalesContext context)
        {
            Random random = new Random();

            string[] customerNames = { "Maria", "Victor", "Vasilka", "Penka", "Todor" };
            string[] emails = { "pp@assdf.bg", "rty(rty@lkj.dsa", "22_asd@asd.asd", "as13df@asdf.lkjhgf.bg" };
            string[] creditCards = { "sd2345698765", "ki0987652345", "mn12345765", "df87652345", "pp98765123456"};

            string[] productNames = { "product_1", "product_2", "product_3", "product_4", "product_5" };

            string[] locationNames = { "Sofia", "Burgas", "Plovdiv", "Varna", "Ruse" };

            for (int i = 0; i < 10; i++)
            {
                context.Customers.AddOrUpdate(new Customer()
                {
                    Name = customerNames[random.Next(customerNames.Length)],
                    Email = emails[random.Next(emails.Length)],
                    CreditCardNumber = creditCards[random.Next(creditCards.Length)]
                });

                context.Products.AddOrUpdate(new Product()
                {
                    Name = productNames[random.Next(productNames.Length)],
                    Price = (decimal)random.NextDouble() * 100m,
                    Quantity = random.Next(50)
                });

                context.StoreLocations.AddOrUpdate(new StoreLocation()
                {
                    LocationName = locationNames[random.Next(locationNames.Length)]
                });
            }

            Product[] products = context.Products.Local.ToArray();
            StoreLocation[] locations = context.StoreLocations.Local.ToArray();
            Customer[] customers = context.Customers.Local.ToArray();

            for (int i = 0; i < 5; i++)
            {
                context.Sales.AddOrUpdate(new Sale()
                {
                    Product = products[random.Next(products.Length)],
                    Customer = customers[random.Next(customers.Length)],
                    StoreLocation = locations[random.Next(locations.Length)],
                    Date = DateTime.Now
                });
            }

            context.SaveChanges();
        }
    }
}
