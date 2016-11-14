using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Linq;

namespace H_04_EntityFramework_CodeFirst.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    class PassValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string password = (string)value;
            HashSet<char> specialCharacters = new HashSet<char>() { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '_', '+', '<', '>', '?' };
            if (password.Any(char.IsLower) && password.Any(char.IsUpper) &&
                 password.Any(char.IsDigit) && password.Any(specialCharacters.Contains))
            {
                return true;             
            }
            else
            {
                return false;
                throw new DbEntityValidationException("Invalid Password");
            }
        }
    }
}
