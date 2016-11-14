using System.ComponentModel.DataAnnotations.Schema;

namespace H_04_EntityFramework_CodeFirst.Models
{
    public partial class User
    {
        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{this.FirstName} {this.LastName}";
            }
        }
    }
}
