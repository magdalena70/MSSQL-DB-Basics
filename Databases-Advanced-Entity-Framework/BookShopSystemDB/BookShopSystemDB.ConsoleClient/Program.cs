using BookShopSystemDB.Data;
using BookShopSystemDB.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.IO;
using System.Linq;

namespace BookShopSystemDB.ConsoleClient
{
    class Program
    {
        static void Main()
        {

            var migrationStrategy = new DropCreateDatabaseIfModelChanges<BookShopContext>();
            Database.SetInitializer(migrationStrategy);

            var context = new BookShopContext();
            var bookCount = context.Books.Count();
            Console.WriteLine(bookCount);

            #region//Step 6 - Write LINQ Queries
            //GetAllBooksAfterYear(context, 2000);
            //GetAllAuthorsWithAtLeastOneBookBeforeYear(context, 1990);
            //GetAllAuthorsOrderedByTheirBooksCountDesc(context);
            //GetAllBooksFromAuthor(context, "George", "Powell");
            //GetTop3MostRecentBooksByCategories(context);
            #endregion
        }

        private static void GetAllBooksFromAuthor(BookShopContext context, string authorFirstName, string authorLastName)
        {
            var books = context.Books
                .Where(b => b.Author.FirstName == authorFirstName && b.Author.LastName == authorLastName)
                .OrderByDescending(b => b.ReleaseDate)
                .ThenBy(b => b.Title)
                .ToList();
            foreach (var book in books)
            {
                Console.WriteLine($"Book {book.Title}, releaseDate: {book.ReleaseDate}, copies: {book.Copies}");
            }
        }

        private static void GetAllBooksAfterYear(BookShopContext context, int year)
        {
            if (year < 1900 || year > DateTime.Now.Year)
            {
                throw new ArgumentOutOfRangeException($"Year must be in range [year > 1900 and year < {DateTime.Now.Year}]");
            }

            var books = context.Books
                .Where(b => b.ReleaseDate.HasValue && b.ReleaseDate.Value.Year > 2000)
                .ToList();
            foreach (var book in books)
            {
                Console.WriteLine(book.Title);
            }
        }

        private static void GetAllAuthorsWithAtLeastOneBookBeforeYear(BookShopContext context, int year)
        {
            if (year < 1900 || year > DateTime.Now.Year)
            {
                throw new ArgumentOutOfRangeException($"Year must be in range [year > 1900 and year < {DateTime.Now.Year}]");
            }

            var authors = context.Authors
                .Where(a => a.Books.Count(b => b.ReleaseDate.HasValue && b.ReleaseDate.Value.Year < 1990) > 0)
                .ToList();
            foreach (var author in authors)
            {
                Console.WriteLine($"{author.FirstName} {author.LastName}");
            }
        }

        private static void GetAllAuthorsOrderedByTheirBooksCountDesc(BookShopContext context)
        {
            var authors = context.Authors
                .OrderByDescending(a => a.Books.Count)
                .ToList();
            foreach (var author in authors)
            {
                Console.WriteLine($"Author {author.FirstName} {author.LastName} -> books count: {author.Books.Count}");
            }
        }

        private static void GetTop3MostRecentBooksByCategories(BookShopContext context)
        {
            var categories = context.Categories
                .OrderByDescending(c => c.Books.Count)
                .Select(c => new
                {
                    c.Name,
                    BooksCount = c.Books.Count,
                    Books = c.Books
                        .OrderByDescending(b => b.ReleaseDate)
                        .ThenBy(b => b.Title)
                        .Take(3)
                        .Select(b => new { b.Title, b.ReleaseDate })
                        .ToList()
                }).ToList();

            foreach (var category in categories)
            {
                Console.WriteLine($"--{category.Name}: {category.BooksCount} books");

                foreach (var book in category.Books)
                {
                    Console.WriteLine($"{book.Title} ({book.ReleaseDate.Value.Year})");
                }
            }
        }
    }
}
