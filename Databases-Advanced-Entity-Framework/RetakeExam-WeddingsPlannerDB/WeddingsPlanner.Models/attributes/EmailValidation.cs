using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WeddingsPlanner.Models.attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EmailValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            bool isValidEmail = true;
            if (value != null)
            {
                string email = (string)value;
                try
                {
                    var r = new Regex(@"([a-z]*\d*^_)+||([a-z]*\d*[a-z]*^_)+||(\d*[a-z]*\d*^_)+@([A-Z]+\.[A-Z]+)+");
                    isValidEmail = r.IsMatch(email);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    isValidEmail = false;
                }
            }

            return isValidEmail;
        }
    }
}
