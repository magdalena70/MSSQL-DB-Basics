namespace Projection.Dto
{
    public class EmployeeDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal Salary { get; set; }

        public string ManagerName { get; set; }

        public override string ToString()
        {
            string result = $"{FirstName} {LastName} {Salary}";
            if(ManagerName != null)
            {
                result += $" - Manager: {ManagerName}";
            }
            else
            {
                result += " - Manager: [no manager]";
            }

            return result;
        }
    }
}
