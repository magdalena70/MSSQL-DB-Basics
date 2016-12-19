using System.ComponentModel.DataAnnotations;
using System;

namespace PhotographyWorkshops.Models.attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MinISO_Validation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            int minIso = (int)value;
            if (minIso < 100)
            {
                Console.WriteLine("Number cannot be lower than 100 or not set.");
                return false;
            }

            return true;
        }
    }
}
