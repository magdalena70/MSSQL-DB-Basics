using System;
using System.Collections.Generic;

namespace AdvancedMapping.Models
{
    public class Employee
    {
        public Employee()
        {
            this.Employees = new List<Employee>();
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal Salary { get; set; }

        public DateTime BirthDay { get; set; }

        public bool IsOnHoliday { get; set; }

        public string Address { get; set; }

        public virtual Employee Manager { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
