using AdvancedMapping.Dto;
using AdvancedMapping.Models;
using AutoMapper;
using System;
using System.Collections.Generic;

namespace AdvancedMapping
{
    class Program
    {
        static void Main()
        {   
            Mapper.Initialize(m => 
            {
                m.CreateMap<Employee, EmployeeDto>();
                m.CreateMap<Employee, ManagerDto>();
            });

            //Object
            Employee manager = CreateManager();
            ManagerDto managerDto = Mapper.Map<ManagerDto>(manager);
            //Console.WriteLine(managerDto);

            //Collection
            ICollection<Employee> managers = CreateManagers(manager);
            ICollection<ManagerDto> managersDto = Mapper.Map<ICollection<ManagerDto>>(managers);
            foreach (var managerFromCollection in managersDto)
            {
                Console.WriteLine(managerFromCollection);
            }
            
        }

        private static ICollection<Employee> CreateManagers(Employee manager)
        {
            ICollection<Employee> managers = new List<Employee>();
            for (int i = 0; i < 5; i++)
            {
                managers.Add(manager);
            }

            return managers;
        }

        private static Employee CreateManager()
        {
            Employee manager = new Employee()
            {
                FirstName = "Angel",
                LastName = "Georgiev",
                Salary = 2300.50m,
                Address = "Sofia 16A",
                BirthDay = new DateTime(1990, 2, 21),
                IsOnHoliday = false,
                Manager = new Employee() { FirstName = "Ivan", LastName = "Ivanov" }
            };

            manager.Employees.Add(new Employee() { FirstName = "Malina", LastName = "Peeva", Salary = 1300.99m });
            manager.Employees.Add(new Employee() { FirstName = "Pencho", LastName = "Stoev", Salary = 1220.58m });
            manager.Employees.Add(new Employee() { FirstName = "Victor", LastName = "Manchev", Salary = 988.44m });
            manager.Employees.Add(new Employee() { FirstName = "Georgi", LastName = "Iliev", Salary = 1200 });
            manager.Employees.Add(new Employee() { FirstName = "Inka", LastName = "Kostova", Salary = 1250 });

            return manager;
        }
    }
}
