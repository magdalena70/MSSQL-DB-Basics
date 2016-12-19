using System;
using System.ComponentModel.DataAnnotations;

namespace PhotographyWorkshops.Models
{
    public class Lens
    {
        [Key]
        public int Id { get; set; }

        public string Make { get; set; }

        public int FocalLength { get; set; }

        public double MaxAperture { get; set; }

        public string CompatibleWith { get; set; }

        public virtual Photographer Owner  { get; set; }
    }
}
