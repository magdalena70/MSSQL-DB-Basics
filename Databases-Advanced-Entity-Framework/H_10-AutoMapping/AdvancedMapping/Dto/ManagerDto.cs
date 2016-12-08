using System.Collections.Generic;

namespace AdvancedMapping.Dto
{
    public class ManagerDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public IList<EmployeeDto> Employees { get; set; }

        public override string ToString()
        {
            string result = $"{FirstName} {LastName} | Employees: {Employees.Count}";
            foreach (var employee in Employees)
            {
                result += $"\n{employee.ToString()}";
            }

            return result;
        }
    }
}
