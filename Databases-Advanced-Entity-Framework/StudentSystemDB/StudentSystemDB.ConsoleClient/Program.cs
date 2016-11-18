using StudentSystemDB.Data;
using StudentSystemDB.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.SqlServer;
using System.Linq;

namespace StudentSystemDB.ConsoleClient
{
    class Program
    {
        static void Main()
        {
            var context = new StudentSystemDBContext();
            context.Database.Initialize(true);

            //ListsAllStudentsAndTheirHomeworks(context);
            //ListAllCoursesWithTheirCorrespondingResources(context);
            //ListAllCoursesWithMoreThanNResources(context, 5);//try with 2
            //ListAllCoursesWhichWereActiveOnAGivenDate(context, DateTime.Today);
            //CalculateStudentCources(context);

            //InsertLincenses(context);
            //InsertStudentsFriends(context);
        }

        private static void ListsAllStudentsAndTheirHomeworks(StudentSystemDBContext context)
        {
            var allStudents = context.Students
                .Select(s => new { Name = s.Name, Homeworks = s.Homeworks })
                .ToList();
            foreach (var student in allStudents)
            {
                Console.WriteLine($"Student {student.Name}, Homeworks:");
                foreach (var homework in student.Homeworks)
                {
                    Console.WriteLine($" --Content: {homework.Content},\n Type: {homework.ContentType};");
                }
            }
        }

        private static void ListAllCoursesWithTheirCorrespondingResources(StudentSystemDBContext context)
        {
            var allCourses = context.Courses
                .OrderBy(c => c.StartDate)
                .ThenByDescending(c => c.EndDate)
                .Select(c => new { Name = c.Name, Description = c.Description, Resourses = c.Resources})
                .ToList();
            foreach (var course in allCourses)
            {
                Console.WriteLine($"\nCourse: {course.Name}, {course.Description}");
                foreach (var resourse in course.Resourses)
                {
                    Console.WriteLine($"-- {resourse.Name}:\n\tType: {resourse.Type}, URL: {resourse.URL}");
                }
            }
        }

        private static void ListAllCoursesWithMoreThanNResources(StudentSystemDBContext context, int numberResources)
        {
            var cources = context.Courses
                .Where(c => c.Resources.Count >= numberResources)
                .OrderByDescending(c => c.Resources.Count)
                .ThenBy(c => c.StartDate)
                .Select(c => new { Name = c.Name, ResourcesCount = c.Resources.Count})
                .ToList();
            if(cources.Count > 0)
            {
                foreach (var course in cources)
                {
                    Console.WriteLine($"Course: {course.Name}, Resources Count: {course.ResourcesCount}");
                }
            }
            else
            {
                Console.WriteLine($"No cources with more than {numberResources} resources ");
            }
        }
        
        private static void ListAllCoursesWhichWereActiveOnAGivenDate(StudentSystemDBContext context, DateTime today)
        {
            var activeCourses = context.Courses
                .Where(c => c.StartDate <= today && c.EndDate >= today)
                .Select(c => new
                    {
                        Name = c.Name,
                        StartDate = c.StartDate,
                        EndDate = c.EndDate,
                        Duration = SqlFunctions.DateDiff("day", c.StartDate, c.EndDate),
                        NumberOfStudents = c.Students.Count
                    }
                )
                .OrderByDescending(c => c.NumberOfStudents)
                .ThenByDescending(c => c.Duration)
                .ToList();
            foreach (var course in activeCourses)
            {
                Console.WriteLine($"\nCourse: {course.Name}:\n from {course.StartDate} - to {course.EndDate}");
                Console.WriteLine($" Duration: {course.Duration} days, Number of students: {course.NumberOfStudents}");
            }
        }

        private static void CalculateStudentCources(StudentSystemDBContext context)
        {
            var studentsWithCources = context.Students
                .Where(s => s.Courses.Count > 0)
                .OrderByDescending(s => s.Courses.Sum(c => c.Price))
                .ThenByDescending(s => s.Courses.Count)
                .ThenBy(s => s.Name)
                .Select(s => new
                {
                    Name = s.Name,
                    CoursesCount = s.Courses.Count,
                    CoursesTotalPrice = s.Courses.Sum(c => c.Price),
                    CoursesAvgPrice = s.Courses.Average(c => c.Price)
                }
                ).ToList();

            foreach (var student in studentsWithCources)
            {
                Console.WriteLine($"Student {student.Name} - {student.CoursesCount} courses:");
                Console.WriteLine($"\tTotal price: {student.CoursesTotalPrice}, AVG price: {student.CoursesAvgPrice}");
            }
        }

        private static void InsertLincenses(StudentSystemDBContext context)
        {
            context.Licenses.AddOrUpdate(l => l.Name,
                new License
                {
                    Name = "This is License about ...Qwertyuio",
                    Resource = context.Resources.Find(1)
                },
                new License
                {
                    Name = "License asdfg poiuy 1234 asdfgh, asdfg.",
                    Resource = context.Resources.Find(1)
                },
                new License
                {
                    Name = "This is License asdf 098 xcvb, zxcvb lkj asdfgh...",
                    Resource = context.Resources.Find(1)
                },
                new License
                {
                    Name = "Asdfgh oiuy zxcvbn 12345 ...12345",
                    Resource = context.Resources.Find(2)
                },
                new License
                {
                    Name = "This is License zxcvbn poiuy - 123yui098765",
                    Resource = context.Resources.Find(2)
                },
                new License
                {
                    Name = "Zxcvbnm - 12345poi098765 zxcvb sdfgh- dfghjk",
                    Resource = context.Resources.Find(3)
                },
                new License
                {
                    Name = "License zxcvbn poiuy",
                    Resource = context.Resources.Find(3)
                },
                new License
                {
                    Name = "Zxcvbnm -License zxcvbn dfghjk",
                    Resource = context.Resources.Find(4)
                },
                new License
                {
                    Name = "QWERT - 12345poi098765 License ",
                    Resource = context.Resources.Find(5)
                }
            );

            context.SaveChanges();
        }

        private static void InsertStudentsFriends(StudentSystemDBContext context)
        {
            var students = context.Students
                .ToList();
            foreach (var student in students)
            {
                foreach (var friend in students)
                {
                    student.Friends.Add(friend);
                }
                
            }

            context.SaveChanges();
        }
    }
}
