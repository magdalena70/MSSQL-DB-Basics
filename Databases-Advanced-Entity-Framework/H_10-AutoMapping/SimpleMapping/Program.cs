using AutoMapper;
using SimpleMapping.Dto;
using SimpleMapping.models;
using System;

namespace SimpleMapping
{
    class Program
    {
        static void Main()
        {
            Employee employee = new Employee()
            {
                FirstName = "Nevena",
                LastName = "Petrova",
                Salary = 899.99m,
                BirthDay = new DateTime(1988, 12, 9),
                Address = "Sofia 1510, Bulgaria"
            };

            Mapper.Initialize(m => 
            {
                m.CreateMap<Employee, EmployeeDto>();
            });
            EmployeeDto employeeDto = Mapper.Map<EmployeeDto>(employee);

            Console.WriteLine("{0} {1} {2}", employeeDto.FirstName, employeeDto.LastName, employeeDto.Salary);
        }
    }
}
