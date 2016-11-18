using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using UniversitySystemDB.Models;

namespace UniversitySystemDB
{
    class Program
    {
        static void Main()
        {
            #region//Table per Hierarchy
            //TphUniversityContext tphContext = new TphUniversityContext();
            //tphContext.Database.Initialize(true);
            //InsertData(tphContext);
            #endregion

            #region//Table per Type
            //TptUniversityContext tptContext = new TptUniversityContext();
            //tptContext.Database.Initialize(true);
            //InsertData(tptContext);
            #endregion

            #region//Table per Concrete Class
            //TpcUniversityContext tpcContext = new TpcUniversityContext();
            //tpcContext.Database.Initialize(true);
            //InsertData(tpcContext);
            #endregion
        }

        private static void InsertData(TpcUniversityContext tpcContext)
        {
            tpcContext.Courses.AddOrUpdate(c => c.Name, new Course
            {
                Name = "Programming Basics",
                Description = "Asdf oiuy cvbn, xcvbn ghjk...",
                StartDate = new DateTime(2016, 7, 20),
                EndDate = new DateTime(2016, 8, 30),
                Credits = 8,
                Teacher = new Teacher
                {
                    Id = 1,
                    FirstName = "Ivan",
                    LastName = "Petrov",
                    Email = "i_van@5rov.bg",
                    PhoneNumber = "0889123456",
                    SalaryPerHour = 25
                },
                Students = new HashSet<Student>()
                {
                    new Student
                    {
                        Id = 2,
                        FirstName = "Georgi",
                        LastName = "Stoyanov",
                        PhoneNumber = "0998231634",
                        AverageGrade = 5.67m,
                        Attendance = "asd"
                    },
                    new Student
                    {
                        Id = 3,
                        FirstName = "Ivana",
                        LastName = "Panova",
                        PhoneNumber = "0999991634",
                        AverageGrade = 5.33m,
                        Attendance = "kkk"
                    },
                    new Student
                    {
                        Id = 4,
                        FirstName = "Milka",
                        LastName = "Stoyanova",
                        PhoneNumber = "0778231934",
                        AverageGrade = 4.50m,
                        Attendance = "jkl"
                    }
                }
            });

            tpcContext.SaveChanges();
        }

        private static void InsertData(TptUniversityContext tptContext)
        {
            tptContext.Courses.AddOrUpdate(c => c.Name, new Course
            {
                Name = "Programming Basics",
                Description = "Asdf oiuy cvbn, xcvbn ghjk...",
                StartDate = new DateTime(2016, 7, 20),
                EndDate = new DateTime(2016, 8, 30),
                Credits = 8,
                Teacher = new Teacher
                {
                    FirstName = "Ivan",
                    LastName = "Petrov",
                    Email = "i_van@5rov.bg",
                    PhoneNumber = "0889123456",
                    SalaryPerHour = 25
                },
                Students = new HashSet<Student>()
                {
                    new Student
                    {
                        FirstName = "Georgi",
                        LastName = "Stoyanov",
                        PhoneNumber = "0998231634",
                        AverageGrade = 5.67m,
                        Attendance = "asd"
                    },
                    new Student
                    {
                        FirstName = "Ivana",
                        LastName = "Panova",
                        PhoneNumber = "0999991634",
                        AverageGrade = 5.33m,
                        Attendance = "kkk"
                    },
                    new Student
                    {
                        FirstName = "Milka",
                        LastName = "Stoyanova",
                        PhoneNumber = "0778231934",
                        AverageGrade = 4.50m,
                        Attendance = "jkl"
                    }
                }
            });

            tptContext.SaveChanges();
        }

        private static void InsertData(TphUniversityContext tphContext)
        {
            tphContext.Courses.AddOrUpdate(c => c.Name, new Course
            {
                Name ="Programming Basics",
                Description = "Asdf oiuy cvbn, xcvbn ghjk...",
                StartDate = new DateTime(2016, 7, 20),
                EndDate = new DateTime(2016, 8, 30),
                Credits = 8,
                Teacher = new Teacher
                {
                    FirstName = "Ivan",
                    LastName = "Petrov",
                    Email = "i_van@5rov.bg",
                    PhoneNumber = "0889123456",
                    SalaryPerHour = 25
                },
                Students = new HashSet<Student>()
                {
                    new Student
                    {
                        FirstName = "Georgi",
                        LastName = "Stoyanov",
                        PhoneNumber = "0998231634",
                        AverageGrade = 5.67m,
                        Attendance = "asd"
                    },
                    new Student
                    {
                        FirstName = "Ivana",
                        LastName = "Panova",
                        PhoneNumber = "0999991634",
                        AverageGrade = 5.33m,
                        Attendance = "kkk"
                    },
                    new Student
                    {
                        FirstName = "Milka",
                        LastName = "Stoyanova",
                        PhoneNumber = "0778231934",
                        AverageGrade = 4.50m,
                        Attendance = "jkl"
                    }
                }
            });

            tphContext.SaveChanges();
        }
    }
}
