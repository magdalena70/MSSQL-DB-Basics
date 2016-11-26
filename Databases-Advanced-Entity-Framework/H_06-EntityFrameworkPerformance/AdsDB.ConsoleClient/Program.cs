using AdsDB.Data;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AdsDB.ConsoleClient
{
    class Program
    {
        static void Main()
        {
            var context = new AdsDBContext();

            #region//Problem 1.Show Data from Related Tables
            //SelectWithoutInclude(context);
            //SelectWithInclude(context);
            #endregion

            #region//Problem 2.Play with ToList()          
            //SelectNonOptimized(context);
            //SelectOptimized(context);
            #endregion

            #region//Problem 3.Select Everything vs. Select Certain Columns
            //TestSelectEverything(context);
            //TestSelectCertainColumns(context);
            #endregion

            #region//4.Test Performance of Order By
            //OrderedBeforeToList(context);
            //OrderedAfterToList(context);
            #endregion
        }

        private static void SelectWithoutInclude(AdsDBContext context)
        {
            context.Ads.Count();
            Stopwatch sw = new Stopwatch();
            sw.Start();

            // Make a lot of queries, however they are done lazy
            var allAds = context.Ads;

            foreach (var ad in allAds)
            {
                Console.WriteLine($"{ad.Title} {ad.AdStatus?.Status} {ad.Category?.Name} {ad.Town?.Name} {ad.AspNetUser?.Name}");
            }

            sw.Stop();
            Console.WriteLine(sw.Elapsed.ToString());
            File.AppendAllText("../../problem_01-results/testInclude.txt",
                sw.Elapsed.ToString() + " Without Include" + Environment.NewLine);
        }

        private static void SelectWithInclude(AdsDBContext context)
        {
            context.Ads.Count();
            Stopwatch sw = new Stopwatch();
            sw.Start();

            // Make one query, however it gets all the data loaded in the memory
            var allAds = context.Ads
                .Include("AdStatus")
                .Include("Category")
                .Include("Town")
                .Include("AspNetUser");

            foreach (var ad in allAds)
            {
                Console.WriteLine($"{ad.Title} {ad.AdStatus?.Status} {ad.Category?.Name} {ad.Town?.Name} {ad.AspNetUser?.Name}");
            }

            sw.Stop();
            Console.WriteLine(sw.Elapsed.ToString());
            File.AppendAllText("../../problem_01-results/testInclude.txt",
                sw.Elapsed.ToString() + " With Include" + Environment.NewLine);
        }

        private static void SelectNonOptimized(AdsDBContext context)
        {
            context.Ads.Count();
            Stopwatch sw = new Stopwatch();
            sw.Start();

            var ads = context.Ads
                .ToList()
                .Where(a => a.AdStatus.Status == "Published")
                .Select(a => new
                {
                    Title = a.Title,
                    Category = a.Category?.Name,
                    Town = a.Town?.Name,
                    Date = a.Date
                })
                .ToList()
                .OrderBy(a => a.Date);

            foreach (var ad in ads)
            {
                Console.WriteLine($"{ad.Title} {ad.Category} {ad.Town} {ad.Date}");
            }

            sw.Stop();
            Console.WriteLine(sw.Elapsed.ToString());
            File.AppendAllText("../../problem_02-results/testToList.txt",
                sw.Elapsed.ToString() + " Non-Optimized" + Environment.NewLine);
        }

        private static void SelectOptimized(AdsDBContext context)
        {
            context.Ads.Count();
            Stopwatch sw = new Stopwatch();
            sw.Start();

            var ads = context.Ads
                .Where(a => a.AdStatus.Status == "Published")
                .OrderBy(a => a.Date)
                .Select(a => new
                {
                    Title = a.Title,
                    Category = a.Category.Name,
                    Town = a.Town.Name,
                    Date = a.Date
                })
                .ToList();

            foreach (var ad in ads)
            {
                Console.WriteLine($"{ad.Title} {ad.Category} {ad.Town} {ad.Date}");
            }

            sw.Stop();
            Console.WriteLine(sw.Elapsed.ToString());
            File.AppendAllText("../../problem_02-results/testToList.txt",
                sw.Elapsed.ToString() + " Optimized" + Environment.NewLine);
        }

        private static void TestSelectEverything(AdsDBContext context)
        {
            context.Ads.Count();
            Stopwatch sw = new Stopwatch();
            sw.Start();

            var ads = context.Ads;
            foreach (var ad in ads)
            {
                Console.WriteLine(ad.Title);
            }

            sw.Stop();
            Console.WriteLine(sw.Elapsed.ToString());
            File.AppendAllText("../../problem_03-results/testSelect.txt",
                sw.Elapsed.ToString() + " Not-Optimized" + Environment.NewLine);
        }

        private static void TestSelectCertainColumns(AdsDBContext context)
        {
            context.Ads.Count();
            Stopwatch sw = new Stopwatch();
            sw.Start();

            var ads = context.Ads
                .Select(a => a.Title);
            foreach (var ad in ads)
            {
                Console.WriteLine(ad);
            }

            sw.Stop();
            Console.WriteLine(sw.Elapsed.ToString());
            File.AppendAllText("../../problem_03-results/testSelect.txt",
                sw.Elapsed.ToString() + " Optimized" + Environment.NewLine);
        }

        private static void OrderedBeforeToList(AdsDBContext context)
        {
            context.Ads.Count();
            Stopwatch sw = new Stopwatch();
            sw.Start();

            var ads = context.Ads
                .OrderBy(a => a.Title)
                .ThenByDescending(a => a.Date)
                .ToList();

            foreach (var ad in ads)
            {
                Console.WriteLine($"{ad.Title} {ad.Date}");
            }

            sw.Stop();
            Console.WriteLine(sw.Elapsed.ToString());
            File.AppendAllText("../../problem_04-results/testOrderBy.txt",
                sw.Elapsed.ToString() + " OrderedBefore" + Environment.NewLine);
        }

        private static void OrderedAfterToList(AdsDBContext context)
        {
            context.Ads.Count();
            Stopwatch sw = new Stopwatch();
            sw.Start();

            //faster than ordered before to list
            var ads = context.Ads
                .ToList()
                .OrderBy(a => a.Title)
                .ThenByDescending(a => a.Date);

            foreach (var ad in ads)
            {
                Console.WriteLine($"{ad.Title} {ad.Date}");
            }

            sw.Stop();
            Console.WriteLine(sw.Elapsed.ToString());
            File.AppendAllText("../../problem_04-results/testOrderBy.txt",
                sw.Elapsed.ToString() + " OrderedAfter" + Environment.NewLine);
        }
    }
}
