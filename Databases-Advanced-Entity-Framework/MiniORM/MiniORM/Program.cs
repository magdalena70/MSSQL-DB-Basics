using MiniORM.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MiniORM
{
    class Program
    {
        static void Main()
        {
            string connectionString = new ConnectionStringBuilder("MiniORM").ConnectionString;
            IDbContext context = new EntityManager(connectionString, true);

            #region//Problem 10 - Test Framework

            ////create and insert
            //List<User> users = new List<User>()
            //{
            //    new User("OldUser", "asdfgh", 48, new DateTime(2009, 10, 22, 17, 20, 33)),
            //    new User("Petranka", "qwert6789", 22, new DateTime(2009, 12, 8, 20, 5, 18)),
            //    new User("Vasil", "zxcv0987", 27, new DateTime(2010, 2, 2, 23, 2, 39)),
            //    new User("Georgi", "098bn", 18, new DateTime(2011, 8, 27, 5, 33, 33)),
            //    new User("Marcheto", "1234xcvb", 17, new DateTime(2013, 11, 22, 2, 37, 3)),
            //    new User("Stanka", "1234_asdf", 35, System.DateTime.Now)
            //};

            //foreach (User user in users)
            //{
            //    context.Persist(user);
            //}

            ////insert and update
            //User newUser = new User("Stephcho", "dragon123", 15, System.DateTime.Now);
            //context.Persist(newUser);
            //newUser.Username = "Stefchy";
            //newUser.Password = "newDragon123";
            //context.Persist(newUser);

            ////findById
            //User userById = context.FindById<User>(1);
            //Console.WriteLine(
            //    $"User by Id {userById.Id} is: \n\t{userById.Username}, Age: {userById.Age}, Id: {userById.Id}");

            ////findAll
            //IEnumerable<User> allUsers = context.FindAll<User>();
            //Console.WriteLine("All users in table Users:");
            //foreach (User user in allUsers)
            //{
            //    Console.WriteLine($"\t{user.Username}, Age: {user.Age} registered: {user.RegistratinDate}");
            //}

            ////findFirst
            //User firstUser = context.FindFirst<User>();
            //Console.WriteLine(
            //    $"First user is: \n\t{firstUser.Username}, Pass: {firstUser.Password}, Id: {firstUser.Id}");

            ////findFirst by condition
            //string findLast = "ORDER BY Id DESC";
            //User lastUser = context.FindFirst<User>(findLast);
            //Console.WriteLine(
            //    $"First user {findLast} is: \n\t{lastUser.Username}, Age: {lastUser.Age}, Id: {lastUser.Id}");

            //deleteById
            //context.DeleteById<Book>(2);
            #endregion

            #region//Problem 11 - Fetch Users

            //string allUsersWhere = "WHERE RegistrationDate > '2010-12-31' AND Age >= 18";
            //IEnumerable<User> selectedUsersWere = context.FindAll<User>(allUsersWhere);

            //Console.WriteLine($"All users in table Users {allUsersWhere}:");
            //foreach (User user in selectedUsersWere)
            //{
            //    Console.WriteLine($"\t{user.Username} - Pass: {user.Password}");
            //    Console.WriteLine($"\tAge: {user.Age} registered on: {user.RegistratinDate}\n");
            //}
            #endregion

            #region////Problem 12 - Add New Entity

            //List<Book> books = new List<Book>()
            //{
            //    new Book("Harry Potter and the Cursed Child - Parts I & II", "J.K. Rowling , Jack Thorne , John Tiffany", new DateTime(2015, 10, 2), "English", true),
            //    new Book("Mansfield Park", "Austen J.", new DateTime(2003, 6, 7), "English", true),
            //    new Book("Merchant of Venice", "Shakespeare W.", new DateTime(2013, 11, 3), "English", false),
            //    new Book("Short Stories from the Nineteenth Century", "Davies D.S.(Ed.)", new DateTime(2011, 12, 4), "English", false),
            //    new Book("The Horror in the Museum: Collected Short Stories Vol.2", "Lovecraft H.P.", new DateTime(2004, 1, 5), "English", true),
            //    new Book("Twenty Thousand Leagues Under the Sea", "Verne J.", new DateTime(2042, 7, 6), "English", false),
            //    new Book("Tale of Two Cities", "Dickens C.", new DateTime(2005, 5, 21), "English", false),
            //    new Book("Adventures & Memoirs of Sherlock Holmes", "Doyle A.C.", new DateTime(2023, 2, 8), "English", true),
            //    new Book("Three Musketeers", "Dumas A.", new DateTime(2012, 1, 30), "English", true),
            //    new Book("Lord Jim", "Conrad J.", new DateTime(2052, 4, 9), "English", false)
            //};

            //foreach (Book book in books)
            //{
            //    context.Persist(book);
            //}

            ////Write a program that check all the books whether their title is over N symbols long 
            ////(where N is a number given as an input) and have hard cover.
            //string input = Console.ReadLine();
            //int titleLength;
            //if (!Int32.TryParse(input, out titleLength))
            //{
            //    throw new Exception($"Invalid input! Length must be an integer.");
            //}

            //IEnumerable<Book> booksCountTitle = context
            //    .FindAll<Book>($"WHERE LEN(Title) >= {titleLength} AND IsHardCovered = 1");
            //int count = 0;
            //foreach (Book book in booksCountTitle)
            //{
            //    if (book != null)
            //    {
            //        //Console.WriteLine(book.Title);
            //        count++;
            //    }
            //}

            //Console.WriteLine($"{count} books now have title with length of {titleLength}");
            #endregion

            #region//Problem 13 - Update Entity

            //List<Book> books = new List<Book>()
            //{
            //    new Book("Harry Potter and the Cursed Child - Parts I & II", "J.K. Rowling , Jack Thorne , John Tiffany", new DateTime(2015, 10, 2), "English", true, 9),
            //    new Book("Mansfield Park", "Austen J.", new DateTime(2003, 6, 7), "English", true, 2),
            //    new Book("Merchant of Venice", "Shakespeare W.", new DateTime(2013, 11, 3), "English", false, 9.2m),
            //    new Book("Short Stories from the Nineteenth Century", "Davies D.S.(Ed.)", new DateTime(2011, 12, 4), "English", false, 3),
            //    new Book("The Horror in the Museum: Collected Short Stories Vol.2", "Lovecraft H.P.", new DateTime(2004, 1, 5), "English", true, 4.5m),
            //    new Book("Twenty Thousand Leagues Under the Sea", "Verne J.", new DateTime(2042, 7, 6), "English", false, 7.9m),
            //    new Book("Tale of Two Cities", "Dickens C.", new DateTime(2005, 5, 21), "English", false, 10),
            //    new Book("Adventures & Memoirs of Sherlock Holmes", "Doyle A.C.", new DateTime(2023, 2, 8), "English", true, 1.9m),
            //    new Book("Three Musketeers", "Dumas A.", new DateTime(2012, 1, 30), "English", true, 9),
            //    new Book("Lord Jim", "Conrad J.", new DateTime(2052, 4, 9), "English", false, 8.5m)
            //};

            //foreach (Book book in books)
            //{
            //    context.Persist(book);
            //}

            //IEnumerable<Book> allUpdatedBooks = context.FindAll<Book>();
            //allUpdatedBooks = allUpdatedBooks
            //    .OrderByDescending(b => b.Rating)
            //    .ThenBy(b => b.Title)
            //    .Take(3);

            //foreach (Book book in allUpdatedBooks)
            //{
            //    Console.WriteLine($"{book.Title} ({book.Author}) - {book.Rating / 10}");
            //}
            #endregion

            #region////Problem 14 - Update Records

            //string input = Console.ReadLine();
            //int year;
            //if (!Int32.TryParse(input, out year))
            //{
            //    throw new InvalidCastException("Invalid input! Year must by an integer.");
            //}

            //string condition = $"WHERE YEAR(PublishedOn) > {year} AND IsHardCovered = 1";
            //IEnumerable<Book> booksReleasedAfterGivenYear = context.FindAll<Book>(condition);
            //int countBook = 0;
            //foreach (Book book in booksReleasedAfterGivenYear)
            //{
            //    book.Title = book.Title.ToUpper();
            //    context.Persist(book);
            //    countBook++;
            //}

            //Console.WriteLine($"Books released after {year} year: {countBook}");
            //foreach (Book book in booksReleasedAfterGivenYear)
            //{
            //    Console.WriteLine(book.Title);
            //}
            #endregion

            #region//Problem 15 - Delete Records

            //IEnumerable<Book> booksWithRatingBelow2 = context.FindAll<Book>("WHERE Rating < 2");
            //int countDeletedBooks = 0;
            //if (booksWithRatingBelow2.Count() > 0)
            //{
            //    foreach (Book book in booksWithRatingBelow2)
            //    {
            //        Console.WriteLine(book.Title);
            //        //delete(current object)
            //        context.Delete<Book>(book);
            //        countDeletedBooks++;
            //    }
            //}

            //Console.Write($"{countDeletedBooks} books has been deleted from the database \n");            
            #endregion

            #region//Problem 16 - Delete Inactive Users

            //List<User> users = new List<User>()
            //{
            //  new User("Gosho", "asd", 12, new DateTime(1992, 3, 30, 12, 15, 19), new DateTime(2015, 3, 5, 14, 12, 12), false),
            //  new User("Pesho", "eiw", 4, new DateTime(2001, 5, 23, 1, 15, 19), new DateTime(2003, 3, 5, 14, 12, 12), false),
            //  new User("Slav", "dda", 42, new DateTime(2005, 7, 28, 2, 15, 19), new DateTime(2016, 3, 5, 14, 12, 12), true),
            //  new User("Bojo", "vcx", 32, new DateTime(2015, 9, 12, 3, 15, 19), new DateTime(2016, 3, 5, 14, 12, 12), false),
            //  new User("Joro", "rew", 22, new DateTime(2013, 2, 13, 3, 15, 19), new DateTime(2013, 3, 5, 14, 12, 12), true),
            //  new User("Katq", "qwe", 44, new DateTime(1999, 2, 15, 4, 15, 19), new DateTime(2000, 3, 5, 14, 12, 12), false),
            //  new User("Rori", "ksa", 52, new DateTime(2014, 1, 19, 5, 15, 19), new DateTime(2014, 3, 5, 14, 12, 12), true),
            //  new User("Emil", "fds", 10, new DateTime(2001, 10, 7, 6, 15, 19), new DateTime(2004, 3, 5, 14, 12, 12), true),
            //  new User("Kolio", "dsa", 36, new DateTime(1995, 7, 2, 12, 15, 19), new DateTime(1999, 3, 5, 14, 12, 12), false),
            //};

            //foreach (var user in users)
            //{
            //    context.Persist(user);
            //}

            //string username = Console.ReadLine();
            //User user = context.FindFirst<User>($"WHERE Username = '{username}'");

            //var timeDifference = DateTime.Now - user.LastLoginTime;
            //double seconds = timeDifference.TotalSeconds;

            //if (seconds < 1)
            //{
            //    Console.WriteLine($"User {username} was last online less than a second");
            //}
            //else if (ConvertSecondsToMinutes(seconds) < 1)
            //{
            //    Console.WriteLine($"User {username} was last online less than a minute");
            //}
            //else if (ConvertSecondsToHours(seconds) < 1)
            //{
            //    Console.WriteLine($"User {username} was last online {(int)ConvertSecondsToMinutes(seconds)} minutes ago");
            //}
            //else if (ConvertSecondsToDays(seconds) < 1)
            //{
            //    Console.WriteLine($"User {username} was last online {(int)ConvertSecondsToHours(seconds)} hours ago");
            //}
            //else if (ConvertSecondsToMonths(seconds) < 1)
            //{
            //    Console.WriteLine($"User {username} was last online {(int)ConvertSecondsToDays(seconds)} days ago");
            //}
            //else if (ConvertSecondsToYears(seconds) < 1)
            //{
            //    Console.WriteLine($"User {username} was last online {(int)ConvertSecondsToMonths(seconds)} moths ago");
            //}
            //else
            //{
            //    Console.WriteLine($"User {username} was last online more than a year");
            //}

            //if (!user.IsActive)
            //{
            //    Console.WriteLine("Would you like to delete that user? (yes/no)");
            //    string confirmation = Console.ReadLine();

            //    if (confirmation.ToLower() == "yes")
            //    {
            //        context.Delete<User>(user);
            //        Console.WriteLine($"User {username} was successfully deleted from the database");
            //    }
            //    else if (confirmation.ToLower() == "no")
            //    {
            //        Console.WriteLine($"User {username} was not deleted from the database");
            //    }
            //    else
            //    {
            //        throw new ArgumentException("Invalid input! Answer must be 'yes' or 'no'.");
            //    }
            //}
            #endregion
        }

        private static double ConvertSecondsToYears(double seconds)
        {
            return ConvertSecondsToMonths(seconds) / 12;
        }

        private static double ConvertSecondsToMonths(double seconds)
        {
            return ConvertSecondsToDays(seconds) / 30;
        }

        private static double ConvertSecondsToDays(double seconds)
        {
            return ConvertSecondsToHours(seconds) / 24;
        }

        private static double ConvertSecondsToHours(double seconds)
        {
            return ConvertSecondsToMinutes(seconds) / 60;
        }

        private static double ConvertSecondsToMinutes(double seconds)
        {
            return seconds / 60;
        }
    }
}
