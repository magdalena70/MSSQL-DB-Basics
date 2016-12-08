using AutoMapper;
using AutoMapper.QueryableExtensions;
using Projection.Dto;
using Projection.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projection
{
    class Program
    {
        static void Main()
        {
            InsertDataInDB();

            Mapper.Initialize(m => 
            {
                m.CreateMap<Employee, EmployeeDto>()
                    .ForMember(dto => dto.ManagerName, conf => conf.MapFrom(emp => emp.Manager.LastName));

            });

            EmployeesContext context = new EmployeesContext();
            var employeesDto = context.Employees
                .Where(e => e.BirthDay.Year < 1990)
                .OrderByDescending(e => e.Salary)
                .ProjectTo<EmployeeDto>()
                .ToList();

            foreach (var emp in employeesDto)
            {
                Console.WriteLine(emp);
            }
        }

        private static void InsertDataInDB()
        {
            ICollection<Employee> employees = CreateManagers();
            EmployeesContext context = new EmployeesContext();
            context.Employees.AddRange(employees);
            context.SaveChanges();
        }

        private static ICollection<Employee> CreateManagers()
        {
            var managers = new List<Employee>();
            Employee manager = new Employee()
            {
                FirstName = "Angel",
                LastName = "Georgiev",
                Salary = 2300.50m,
                Address = "Sofia 16A",
                BirthDay = new DateTime(1970, 2, 21),
                Manager = new Employee()
                {
                    FirstName = "Ivan",
                    LastName = "Ivanov",
                    Salary = 1300,
                    BirthDay = new DateTime(1972, 2, 13)
                }
            };

            manager.Employees.Add(new Employee() { FirstName = "Malina", LastName = "Peeva", Salary = 1300.99m, BirthDay = new DateTime(1988, 5, 17) });
            manager.Employees.Add(new Employee() { FirstName = "Pencho", LastName = "Stoev", Salary = 1220.58m, BirthDay = new DateTime(1987, 5, 17) });
            manager.Employees.Add(new Employee() { FirstName = "Victor", LastName = "Manchev", Salary = 988.44m, BirthDay = new DateTime(1983, 10, 22) });
            manager.Employees.Add(new Employee() { FirstName = "Georgi", LastName = "Iliev", Salary = 1200, BirthDay = new DateTime(1978, 8, 9) });
            manager.Employees.Add(new Employee() { FirstName = "Inka", LastName = "Kostova", Salary = 1250, BirthDay = new DateTime(1988, 3, 19) });

            Employee employee = new Employee()
            {
                FirstName = "Vanina",
                LastName = "Vanini",
                Salary = 1200.23m,
                Address = "Pleven, Kokiche 15",
                BirthDay = new DateTime(1992, 7, 13),
                Manager = new Employee()
                {
                    FirstName = "Vasil",
                    LastName = "Petrov",
                    Salary = 880,
                    BirthDay = new DateTime(1982, 3, 22),
                    Manager = new Employee()
                    {
                        FirstName = "Pavel",
                        LastName = "Matev",
                        Salary = 1350.20m,
                        Address = "Varna, Ivan Shishmanov 50",
                        BirthDay = new DateTime(1980, 10, 2),
                        Manager = new Employee()
                        {
                            FirstName = "Pesho",
                            LastName = "Peshev",
                            Salary = 3000,
                            Address = "Sofia, Vasil Kanchev 22",
                            BirthDay = new DateTime(1981, 5, 30)
                        }
                    }
                }
            };

            managers.Add(manager);
            managers.Add(employee);

            return managers;
        }
    }
}
