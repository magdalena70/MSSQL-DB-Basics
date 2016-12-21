using System.Collections.Generic;

namespace WeddingsPlanner.InportJson.Dtos
{
    public class WeddingDto
    {
        public string Bride { get; set; }

        public string Bridegroom { get; set; }

        public string Date { get; set; }

        public string Agency { get; set; }

        public ICollection<GuestDto> Guests { get; set; }
    }
}
