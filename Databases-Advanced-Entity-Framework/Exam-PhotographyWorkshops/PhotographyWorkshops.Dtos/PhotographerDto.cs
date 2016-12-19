using System.Collections.Generic;

namespace PhotographyWorkshops.Dtos
{
    public class PhotographerDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public ICollection<int> Lenses { get; set; }
    }
}
