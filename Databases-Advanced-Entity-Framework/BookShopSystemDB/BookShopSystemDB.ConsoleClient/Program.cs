using BookShopSystemDB.Data;
using BookShopSystemDB.Models;
using EntityFramework.Extensions;
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

            #region//Homework_06 - Advanced Querying

            //SelectBooksTitlesByAgeRestriction(context);
            //SelectGoldenBooks(context);
            //SelectBooksByPrice(context);
            //SelectNotReleasedBooks(context);
            //SelectBookTitlesByCategory(context);
            //SelectBookTitlesByCategory_Variant2(context);
            //SelectBooksReleasedBeforeDate(context);
            //AuthorsSearch(context);
            //BooksSearch(context);
            //BookTitlesSearch(context);
            //CountBooks(context);
            //SelectTotalBookCopiesByAuthor(context);
            //FindTotalProfitOfAllBooksByCategory(context);
            //SelectMostRecentBooks(context, 35); // try with 2
            //IncreaseBookCopies(context);
            //RemoveBooks(context);
            #endregion
        }

        #region//Homework_06
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
        private static void SelectBookTitlesByCategory_Variant2(BookShopContext context)
        {
            var givenCategories = Console.ReadLine().Split(' ').ToList();
            var books = context.Books
                .Where(b => b.Categories.Count(c => givenCategories.Contains(c.Name)) != 0)
                .Select(b => b.Title);

            foreach (var bookTitle in books)
            {
                Console.WriteLine(bookTitle);
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

        private static void BookTitlesSearch(BookShopContext context)
        {
            string givenString = Console.ReadLine().Trim();
            var books = context.Books
                .Where(b => b.Author.LastName.StartsWith(givenString))
                .Select(b => new { Title = b.Title, Author = b.Author.FirstName + " " + b.Author.LastName});

            foreach (var book in books)
            {
                Console.WriteLine($"{book.Title} ({book.Author})");
            }
        }

        private static void CountBooks(BookShopContext context)
        {
            string givenNumberStr = Console.ReadLine();
            int givenNumber;
            if (Int32.TryParse(givenNumberStr, out givenNumber))
            {
                var books = context.Books
                    .Where(b => b.Title.Length > givenNumber)
                    .Select(b => b.Title);
                Console.WriteLine(books.Count());
                Console.WriteLine($"There are {books.Count()} books with longer title than {givenNumber} symbols");
            }
            else
            {
                Console.WriteLine("Invalid input!");
            }
        }

        private static void SelectTotalBookCopiesByAuthor(BookShopContext context)
        {
            var booksByAuthor = context.Books
                .GroupBy(b => b.Author)
                .OrderByDescending(b => b.Sum(book => book.Copies));

            foreach (var books in booksByAuthor)
            {
                Console.WriteLine($"{books.Key.FirstName} {books.Key.LastName} - {books.Sum(b => b.Copies)}");
            }
        }

        private static void FindTotalProfitOfAllBooksByCategory(BookShopContext context)
        {
            var categoryInfo = context.Categories
                .GroupBy(c => new
                {
                    CategoryName = c.Name, 
                    Profit = c.Books.Sum(b => b.Copies * b.Price)
                })
                .OrderByDescending(c => c.Key.Profit)
                .ThenBy(c => c.Key.CategoryName);

            foreach (var category in categoryInfo)
            {
                Console.WriteLine($"{category.Key.CategoryName} - ${category.Key.Profit}");
            }
        }

        private static void SelectMostRecentBooks(BookShopContext context, int booksCount)
        {
            var categories = context.Categories
                .Where(c => c.Books.Count > booksCount)
                .Select(c => new
                {
                    CategotyName = c.Name,
                    BooksCount = c.Books.Count,
                    Books = c.Books
                        .OrderByDescending(b => b.ReleaseDate)
                        .ThenBy(b => b.Title)
                        .Select(b => new { b.Title, b.ReleaseDate })
                        .Take(3)
                })
                .ToList();

            foreach (var category in categories)
            {
                Console.WriteLine($"--{category.CategotyName}: {category.BooksCount} books");
               
                foreach (var book in category.Books)
                {
                    Console.WriteLine($"{book.Title} ({book.ReleaseDate.Value.Year})");
                }
            }
        }

        private static void IncreaseBookCopies(BookShopContext context)
        {
            string givenDateStr = Console.ReadLine();
            DateTime givenDate = DateTime.Parse(givenDateStr);
            int numberOfBookCopiesToIncrease = int.Parse(Console.ReadLine());

            var booksReleasedAfterGivenDate = context.Books
                .Where(b => b.ReleaseDate > givenDate);

            int booksCount = booksReleasedAfterGivenDate.Count();
            int totalAmount = booksReleasedAfterGivenDate.Count() * numberOfBookCopiesToIncrease;
            Console.WriteLine($"{totalAmount}");

            //using EntityFramework.Extended
            context.Books.Update(booksReleasedAfterGivenDate, 
                b => new Book() { Copies = b.Copies + numberOfBookCopiesToIncrease });
            context.SaveChanges();

            Console.WriteLine($"{booksCount} books are released after {givenDateStr} so total of {totalAmount} book copies were added");
        }

        private static void RemoveBooks(BookShopContext context)
        {
            int givenNumberOfCopies = int.Parse(Console.ReadLine());

            var books = context.Books
                .Where(b => b.Copies < givenNumberOfCopies);

            //using EntityFramework.Extended
            context.Books.Delete(books);
            context.SaveChanges();

            Console.WriteLine($"{books.Count()} books were deleted");
        }
        #endregion

        #region//Homework_05 Step 6
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
        #endregion
    }
}
