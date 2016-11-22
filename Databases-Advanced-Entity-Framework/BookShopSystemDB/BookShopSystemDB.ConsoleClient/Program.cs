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
            //var bookCount = context.Books.Count();
            //Console.WriteLine(bookCount);

            #region//Homework_05 Step 6 - Write LINQ Queries
            //GetAllBooksAfterYear(context, 2000);
            //GetAllAuthorsWithAtLeastOneBookBeforeYear(context, 1990);
            //GetAllAuthorsOrderedByTheirBooksCountDesc(context);
            //GetAllBooksFromAuthor(context, "George", "Powell");
            //GetTop3MostRecentBooksByCategories(context);
            #endregion

            #region//Homework_05 Step 7 - Related Books

            ////insert RelatedBooks
            //var books = context.Books
            //    .Take(3)
            //    .ToList();
            //books[0].RelatedBooks.Add(books[1]);
            //books[1].RelatedBooks.Add(books[0]);
            //books[0].RelatedBooks.Add(books[2]);
            //books[2].RelatedBooks.Add(books[0]);

            //context.SaveChanges();

            //// Query the first three books to get their names
            //// and their related book names
            //var booksFromQuery = context.Books
            //    .Take(3)
            //    .ToList();
            //foreach (var book in booksFromQuery)
            //{
            //    Console.WriteLine("--{0}", book.Title);
            //    foreach (var relatedBook in book.RelatedBooks)
            //    {
            //        Console.WriteLine(relatedBook.Title);
            //    }
            //}
            #endregion

            #region//Advanced Querying from Homework_06

            //SelectBooksTitlesByAgeRestriction(context);
            //SelectGoldenBooks(context);
            //SelectBooksByPrice(context);
            //SelectNotReleasedBooks(context);
            //SelectBookTitlesByCategory(context);
            //SelectBooksReleasedBeforeDate(context);
            //AuthorsSearch(context);
            BooksSearch(context);
            #endregion
        }

        //Homework_06
        private static void SelectBooksTitlesByAgeRestriction(BookShopContext context)
        {
            string ageRestrictionsStr = Console.ReadLine().Trim().ToLower();

            if (ageRestrictionsStr != "minor" && ageRestrictionsStr != "teen" && ageRestrictionsStr != "adult")
            {
                throw new ArgumentOutOfRangeException("AgeRestriction can be: 'minor', 'teen' or 'adult'");
            }

            int ageRestriction = GetInputAgeRestrictionValue(ageRestrictionsStr);

            var books = context.Books
                .Where(b => (int)b.AgeRestriction == ageRestriction)
                .Select(b => b.Title);
            foreach (var bookTitle in books)
            {
                Console.WriteLine(bookTitle);
            }
        }

        private static int GetInputAgeRestrictionValue(string ageRestrictionsStr)
        {
            if (ageRestrictionsStr == "minor")
            {
                return 0;
            }
            else if (ageRestrictionsStr == "teen")
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }

        private static void SelectGoldenBooks(BookShopContext context)
        {
            var goldenBooks = context.Books
                .Where(b => (int)b.EditionType == 2 && b.Copies < 5000)
                .Select(b => b.Title);

            foreach (var bookTitle in goldenBooks)
            {
                Console.WriteLine(bookTitle);
            }
        }

        private static void SelectBooksByPrice(BookShopContext context)
        {
            var bookByPrice = context.Books
                .Where(b => b.Price < 5 || b.Price > 40)
                .Select(b => new { Title = b.Title, Price = b.Price });

            foreach (var book in bookByPrice)
            {
                Console.WriteLine($"{book.Title} {book.Price}");
            }
        }

        private static void SelectNotReleasedBooks(BookShopContext context)
        {
            string givenYearStr = Console.ReadLine();
            int givenYear;
            if (givenYearStr.Length == 4 && Int32.TryParse(givenYearStr, out givenYear))
            {
                var notReleasedBooks = context.Books
                    .Where(b => b.ReleaseDate.Value.Year < givenYear)
                    .Select(b => b.Title);

                foreach (var bookTitle in notReleasedBooks)
                {
                    Console.WriteLine(bookTitle);
                }
            }
            else
            {
                Console.WriteLine("Invalid input!");
            }
        }

        private static void SelectBookTitlesByCategory(BookShopContext context)
        {
            string[] categories = Console.ReadLine()
                .Trim()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string category in categories)
            {
                string categoryLower = category.Trim().ToLower();
                Console.WriteLine($"Category: {categoryLower}");

                var booksByCategory = context.Categories
                    .Where(c => c.Name.ToLower() == categoryLower)
                    .Select(c => c.Books)
                    .ToList();

                foreach (var books in booksByCategory)
                {
                    foreach (var book in books)
                    {
                        Console.WriteLine($"\t{book.Title}");
                    }       
                }
            }
        }

        private static void SelectBooksReleasedBeforeDate(BookShopContext context)
        {
            string[] givenDate = Console.ReadLine()
                .Trim()
                .Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries);

            int year;
            int month;
            int day;
            if (Int32.TryParse(givenDate[2], out year) && 
                Int32.TryParse(givenDate[1], out month) &&
                Int32.TryParse(givenDate[0], out day))
            {
                try
                {
                    var books = context.Books
                        .Where(b => b.ReleaseDate < new DateTime(year, month, day))
                        .Select(b => new { Title = b.Title, EditionType = b.EditionType, Price = b.Price });

                    foreach (var book in books)
                    {
                        Console.WriteLine($"{book.Title} {book.EditionType} {book.Price}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Invalid input! {0}", ex.Message);
                    //throw new ArgumentOutOfRangeException(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Invalid input!");
            }
        }

        private static void AuthorsSearch(BookShopContext context)
        {
            string givenString = Console.ReadLine();
            if (givenString != null)
            {
                var authors = context.Authors
                    .Where(a => a.FirstName.EndsWith(givenString))
                    .Select(a => new { FullName = a.FirstName + " " + a.LastName });

                foreach (var author in authors)
                {
                    Console.WriteLine(author.FullName);
                }
            }
        }

        private static void BooksSearch(BookShopContext context)
        {
            string givenString = Console.ReadLine().ToLower();
            if(givenString != null)
            {
                var books = context.Books
                    .Where(b => b.Title.ToLower().Contains(givenString))
                    .Select(b => b.Title);

                foreach (var bookTitle in books)
                {
                    Console.WriteLine(bookTitle);
                }
            }
        }


        //Homework_05 Step 6
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
