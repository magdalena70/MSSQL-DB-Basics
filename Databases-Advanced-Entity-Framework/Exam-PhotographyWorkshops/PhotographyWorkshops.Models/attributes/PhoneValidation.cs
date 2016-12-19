using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PhotographyWorkshops.Models.attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PhoneValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
                string phone = (string)value;
            try
            {   
                var r = new Regex(@"\+\d{1,3}\/\d{8,10}");
                return r.IsMatch(phone);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
