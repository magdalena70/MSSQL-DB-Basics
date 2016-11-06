using SoftUni.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftUni
{
    class Program
    {
        static void Main()
        {
            SoftuniContext context = new SoftuniContext();
            using (context) {
                #region//Problem 3-Employees full information
                IEnumerable<Employee> employees = context.Employees;
                foreach (Employee emp in employees)
                {
                    Console.WriteLine($"{emp.FirstName} {emp.LastName} {emp.MiddleName} {emp.JobTitle} {emp.Salary.ToString(CultureInfo.CreateSpecificCulture("en-US"))}");
                }
                #endregion

                #region//Problem 4-Employees with Salary Over 50 000
                //IEnumerable<string> employeesNames = context.Employees
                //    .Where(e => e.Salary > 50000).Select(e => e.FirstName);
                //foreach (string name in employeesNames)
                //{
                //    Console.WriteLine(name);
                //}
                #endregion

                #region//Problem 5-Employees from Seattle
                //IEnumerable<Employee> employeesFromSeattle = context.Employees
                //    .Where(e => e.Department.Name == "Research and Development")
                //    .OrderBy(e => e.Salary)
                //    .ThenByDescending(e => e.FirstName);
                //foreach (Employee emp in employeesFromSeattle)
                //{

                //    Console.WriteLine($"{emp.FirstName} {emp.LastName} from {emp.Department.Name} - ${emp.Salary:F2}");
                //}
                #endregion

                #region//Problem 6-Adding a New Address and Updating Employee
                ////add new address
                //Address newAddress = new Address();
                //newAddress.AddressText = "Vitoshka 15";
                //newAddress.TownID = 4;

                //Employee employeeNakov = context.Employees
                //    .FirstOrDefault(e => e.LastName == "Nakov");
                //employeeNakov.Address = newAddress;

                //context.SaveChanges();
                ////print result
                //IEnumerable<string> employeesByAddress = context.Employees
                //    .OrderByDescending(e => e.AddressID)
                //    .Take(10)
                //    .Select(e => e.Address.AddressText);
                //foreach (var address in employeesByAddress)
                //{
                //    Console.WriteLine(address);
                //}
                #endregion

                #region//Problem 7-Delete Project by Id

                //Project projectToDelete = context.Projects.Find(2);
                ////first remove from employees (FK)
                //IEnumerable<Employee> projectEmployees = projectToDelete.Employees;
                //foreach (Employee employee in projectEmployees)
                //{
                //    employee.Projects.Remove(projectToDelete);
                //}
                //context.SaveChanges();

                ////than remove from projects
                //context.Projects.Remove(projectToDelete);
                //context.SaveChanges();

                ////print result
                //IEnumerable<string> projects = context.Projects
                //    .Take(10)
                //    .Select(p => p.Name);
                //foreach (string name in projects)
                //{
                //    Console.WriteLine(name);
                //}
                #endregion

                #region//Problem 8-Find employees in period

                //CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
                //StringBuilder result = new StringBuilder();
                //var employees = context.Employees
                //    .Where(employee => employee.Projects
                //        .Count(project => project.StartDate.Year >= 2001 && project.StartDate.Year <= 2003) > 0)
                //    .Take(30);

                //foreach (var employee in employees)
                //{
                //    result.AppendLine($"{employee.FirstName} {employee.LastName} {employee.Manager.FirstName}");
                //    foreach (Project p in employee.Projects)
                //    {
                //        result.AppendLine($"--{p.Name} {p.StartDate} {p.EndDate}");
                //    }
                //}

                //Console.WriteLine(result.ToString());
                #endregion

                #region//Problem 9-Addresses by town name

                //IEnumerable<Address> addresses = context.Addresses
                //    .OrderByDescending(a => a.Employees.Count)
                //    .ThenBy(a => a.Town.Name)
                //    .Take(10);
                //foreach (var address in addresses)
                //{
                //    Console.WriteLine($"{address.AddressText}, {address.Town.Name} - {address.Employees.Count} employees");
                //}
                #endregion

                #region//Problem 10-Employee with id 147 sorted by project names

                //Employee employeeById = context.Employees.Find(147);
                //IEnumerable<Project> employeeProjects = employeeById.Projects
                //    .OrderBy(p => p.Name);

                //Console.WriteLine($"{employeeById.FirstName} {employeeById.LastName} {employeeById.JobTitle}");
                //foreach (var project in employeeProjects)
                //{
                //    Console.WriteLine($"{project.Name}");
                //}
                #endregion

                #region//Problem 11-Departments with more than 5 employees

                //IEnumerable<Department> departments = context.Departments
                //    .Where(d => d.Employees.Count > 5)
                //    .OrderBy(d => d.Employees.Count);
                //foreach (var d in departments)
                //{
                //    Console.WriteLine($"{d.Name} {d.Employee.FirstName}");
                //    foreach (var e in d.Employees)
                //    {
                //        Console.WriteLine($"{e.FirstName} {e.LastName} {e.JobTitle}");
                //    }
                //}
                #endregion

                #region//Problem 15-Find Latest 10 Projects

                //CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
                //StringBuilder resultInFile = new StringBuilder();
                //IEnumerable<Project> latestTenStartedProjects = context.Projects
                //    .OrderByDescending(p => p.StartDate).Take(10)
                //    .OrderBy(p => p.Name);
                //foreach (var project in latestTenStartedProjects)
                //{
                //    resultInFile.AppendLine($"{project.Name} {project.Description} {project.StartDate} {project.EndDate}");
                //}

                //File.WriteAllText("../../latestTenStartedProjects.txt", resultInFile.ToString());
                #endregion

                #region//Problem 16-Increase Salaries

                //var employees = context.Employees
                //    .Where(e => e.Department.Name == "Engineering" ||
                //                e.Department.Name == "Tool Design" ||
                //                e.Department.Name == "Marketing " ||
                //                e.Department.Name == "Information Services");
                //foreach (var emp in employees)
                //{
                //    emp.Salary = emp.Salary * 1.12m;
                //    Console.WriteLine($"{emp.FirstName} {emp.LastName} (${emp.Salary})");
                //}

                //context.SaveChanges();
                #endregion

                #region//Problem 17-Remove Towns

                //string townName = Console.ReadLine();
                //Town townByName = context.Towns
                //    .FirstOrDefault(t => t.Name == townName);
                //if (townByName == null)
                //{
                //    throw new NullReferenceException("No town whit that name.");
                //}

                //IList<Address> addressesByTown = townByName.Addresses.ToList();
                //foreach (var address in addressesByTown)
                //{
                //    IList<Employee> employeesByAddress = address.Employees.ToList();
                //    foreach (var employee in employeesByAddress)
                //    {
                //        employee.AddressID = null;
                //    }
                //}

                //context.Addresses.RemoveRange(addressesByTown);
                //context.Towns.Remove(townByName);
                //context.SaveChanges();  
                //if (addressesByTown.Count() == 1)
                //{
                //    Console.WriteLine($"1 address in {townName} was deleted");
                //}
                //else
                //{
                //    Console.WriteLine($"{addressesByTown.Count()} addresses  in {townName} were deleted");
                //}
                #endregion

                #region//Problem 18-Find Employees by First Name starting with ‘SA’

                //IEnumerable<Employee> employeesByFirstName = context.Employees
                //    .Where(e => e.FirstName.StartsWith("SA"));
                //foreach (Employee emp in employeesByFirstName)
                //{
                //    Console.WriteLine($"{emp.FirstName} {emp.LastName} – {emp.JobTitle} - (${emp.Salary})");
                //}
                #endregion
            }
        }
    }
}
