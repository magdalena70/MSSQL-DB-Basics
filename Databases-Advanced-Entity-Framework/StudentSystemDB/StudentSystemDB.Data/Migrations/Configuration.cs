namespace StudentSystemDB.Data.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<StudentSystemDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "StudentSystemDBContext";
        }

        protected override void Seed(StudentSystemDBContext context)
        {
            AddOrUpdateStudents(context);
            AddOrUpdateCourses(context);
            AddStudentCourses(context);
            AddOrUpdateHomeworks(context);
            AddOrUpdateResources(context);
        }

        private void AddOrUpdateStudents(StudentSystemDBContext context)
        {
            context.Students.AddOrUpdate(s => s.Name,
                new Student
                {
                    Name = "Grogor",
                    Birthday = new DateTime(1988, 10, 23),
                    PhoneNumber = "1234567890",
                    RegistrationDate = DateTime.Now
                },
                new Student
                {
                    Name = "Elena",
                    Birthday = new DateTime(1992, 8, 13),
                    PhoneNumber = "1238887890",
                    RegistrationDate = DateTime.Now
                },
                new Student
                {
                    Name = "Victor",
                    Birthday = new DateTime(1978, 1, 10),
                    PhoneNumber = "1943887890",
                    RegistrationDate = DateTime.Now
                }
            );

            context.SaveChanges();
        }
        private void AddOrUpdateCourses(StudentSystemDBContext context)
        {
            context.Courses.AddOrUpdate(c => c.Name, 
                new Course
                {
                    Name = "EntityFramework Advanced",
                    Description = "Some description....asdfghj fghjk, poiuytr.",
                    StartDate = new DateTime(2015, 12, 8),
                    EndDate = new DateTime(2016, 4, 30),
                    Price = 320
                },
                new Course
                {
                    Name = "Linux System Administration",
                    Description = "Some description....oiuy poi, zxcvbn...",
                    StartDate = new DateTime(2016, 10, 18),
                    EndDate = new DateTime(2016, 12, 22),
                    Price = 250.50m
                },
                new Course
                {
                    Name = "Embedded Systems Development",
                    Description = "Some description....zxcvbn.",
                    StartDate = new DateTime(2016, 7, 1),
                    EndDate = new DateTime(2016, 11, 30),
                    Price = 500
                }
            );

            context.SaveChanges();
        }
        private void AddStudentCourses(StudentSystemDBContext context)
        {
            var students = context.Students.ToList();
            var courses = context.Courses.ToList();
            foreach (var student in students)
            {
                foreach (var course in courses)
                {
                    course.Students.Add(student);
                }
            }

            context.SaveChanges();
        }
        private void AddOrUpdateHomeworks(StudentSystemDBContext context)
        {
            context.Homeworks.AddOrUpdate(h => h.Content, 
                new Homework
                {
                    Content = "Some content.....about Entity Framework...asdfgh...",
                    ContentType = ContentTypes.Application,
                    SubmissionDate = new DateTime(2016, 1, 20),
                    Course = context.Courses.Find(1),
                    Student = context.Students.Find(1)
                },
                new Homework
                {
                    Content = "Any content.....about Entity Framework...asdfg zxcvbn, poiuyt dfg zxcvb...",
                    ContentType = ContentTypes.Application,
                    SubmissionDate = new DateTime(2016, 2, 21),
                    Course = context.Courses.Find(1),
                    Student = context.Students.Find(1)
                },
                new Homework
                {
                    Content = "Any content.....about Linux System Administration poiu, zxcvbn dfg zxcvb...",
                    ContentType = ContentTypes.Zip,
                    SubmissionDate = new DateTime(2016, 11, 21),
                    Course = context.Courses.Find(2),
                    Student = context.Students.Find(1)
                },
                new Homework
                {
                    Content = "Content about Linux System Administration poiu, zxcvbn dfg zxcvb...",
                    ContentType = ContentTypes.Zip,
                    SubmissionDate = new DateTime(2016, 11, 22),
                    Course = context.Courses.Find(2),
                    Student = context.Students.Find(2)
                },
                new Homework
                {
                    Content = "Content about Embedded Systems Development, zxcvbn dfg zxcvb...",
                    ContentType = ContentTypes.Pdf,
                    SubmissionDate = new DateTime(2016, 8, 5),
                    Course = context.Courses.Find(3),
                    Student = context.Students.Find(3)
                }
            );

            context.SaveChanges();
        }
        private void AddOrUpdateResources(StudentSystemDBContext context)
        {
            context.Resources.AddOrUpdate(r => r.Name, 
                new Resource
                {
                    Name = "Resourse_1 asdfghjk",
                    Type = TypeOfResource.Document,
                    URL = "https://www.google.bg/asdfghjk",
                    Course = context.Courses.Find(1)
                },
                new Resource
                {
                    Name = "Resourse_2 ASDf POIUYt",
                    Type = TypeOfResource.Video,
                    URL = "https://www.google.bg/ASDf-POIUYt",
                    Course = context.Courses.Find(1)
                },
                new Resource
                {
                    Name = "Resourse_1 ASDFgh POIuy ZXCvbn",
                    Type = TypeOfResource.Presentation,
                    URL = "https://www.google.bg/ASDFgh-POIuy-ZXCvbn",
                    Course = context.Courses.Find(2)
                },
                new Resource
                {
                    Name = "Resourse_2 ASDFgh POIuy",
                    Type = TypeOfResource.Document,
                    URL = "https://www.google.bg/ASDFgh-POIuy",
                    Course = context.Courses.Find(2)
                },
                new Resource
                {
                    Name = "Resourse_1 PKasdf ASDfgh GG zxcvb",
                    Type = TypeOfResource.Presentation,
                    URL = "https://www.google.bg/PKasdf-ASDfgh-GG-zxcvb",
                    Course = context.Courses.Find(3)
                },
                new Resource
                {
                    Name = "Resourse_2 kpk BB",
                    Type = TypeOfResource.Other,
                    URL = "https://www.google.bg/kpk-BB",
                    Course = context.Courses.Find(3)
                }
            );

            context.SaveChanges();
        }

    }
}
