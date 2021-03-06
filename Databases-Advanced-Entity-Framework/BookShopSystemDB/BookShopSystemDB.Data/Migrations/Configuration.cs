namespace BookShopSystemDB.Data.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Globalization;
    using System.IO;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BookShopSystemDB.Data.BookShopContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "BookShopSystemDB.Data.BookShopContext";
        }

        protected override void Seed(BookShopSystemDB.Data.BookShopContext context)
        {
            Random random = new Random();
            SeedAuthors(context);
            var authors = context.Authors.ToList();
            SeedBooks(context, random, authors);
            SeedCategories(context, random);
        }

        private static void SeedAuthors(BookShopContext context)
        {
            using (var reader = new StreamReader("C:\\Users\\acer\\Documents\\GitHub\\MSSQL-DB-Basics\\Databases-Advanced-Entity-Framework\\BookShopSystemDB\\BookShopSystemDB.Data\\resources\\authors.txt"))
            {
                var line = reader.ReadLine();
                line = reader.ReadLine();
                while (line != null)
                {
                    var data = line.Split(new[] { ' ' });
                    var firstName = data[0];
                    var lastName = data[1];

                    context.Authors.AddOrUpdate(author => author.FirstName,
                        new Author()
                        {
                            FirstName = firstName,
                            LastName = lastName
                        });

                    line = reader.ReadLine();
                }
            }

            context.SaveChanges();
        }

        private static void SeedBooks(BookShopContext context, Random random, List<Author> authors)
        {
            using (var reader = new StreamReader("C:\\Users\\acer\\Documents\\GitHub\\MSSQL-DB-Basics\\Databases-Advanced-Entity-Framework\\BookShopSystemDB\\BookShopSystemDB.Data\\resources\\books.txt"))
            {
                var line = reader.ReadLine();
                line = reader.ReadLine();
                while (line != null)
                {
                    var data = line.Split(new[] { ' ' }, 6);
                    var authorIndex = random.Next(0, authors.Count);
                    var author = authors[authorIndex];
                    var edition = (EditionTypes)int.Parse(data[0]);
                    var releaseDate = DateTime.ParseExact(data[1], "d/M/yyyy", CultureInfo.InvariantCulture);
                    var copies = int.Parse(data[2]);
                    var price = decimal.Parse(data[3]);
                    var ageRestriction = (AgeRestrictions)int.Parse(data[4]);
                    var title = data[5];

                    context.Books.AddOrUpdate(book => book.Title,
                        new Book()
                        {
                            Author = author,
                            EditionType = edition,
                            ReleaseDate = releaseDate,
                            Copies = copies,
                            Price = price,
                            AgeRestriction = ageRestriction,
                            Title = title
                        });

                    line = reader.ReadLine();
                }
            }

            context.SaveChanges();
        }

        private static void SeedCategories(BookShopContext context, Random random)
        {
            using (var reader = new StreamReader("C:\\Users\\acer\\Documents\\GitHub\\MSSQL-DB-Basics\\Databases-Advanced-Entity-Framework\\BookShopSystemDB\\BookShopSystemDB.Data\\resources\\categories.txt"))
            {
                var line = reader.ReadLine();
                line = reader.ReadLine();
                while (line != null)
                {
                    var data = line.Split(new[] { ' ' });
                    var name = data[0];
                    var books = context.Books.ToArray();
                    HashSet<Book> booksToAdd = new HashSet<Book>()
                    {
                        books[random.Next(books.Length)],
                        books[random.Next(books.Length)],
                        books[random.Next(books.Length)],
                    };


                    context.Categories.AddOrUpdate(cat => cat.Name,
                        new Category()
                        {
                            Name = name,
                            Books = booksToAdd
                        });

                    line = reader.ReadLine();
                }
            }

            context.SaveChanges();
        }
    }

}
