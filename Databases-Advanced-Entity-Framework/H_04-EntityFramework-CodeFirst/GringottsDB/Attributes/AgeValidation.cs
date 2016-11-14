using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;

namespace GringottsDB.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    class AgeValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            int age = (int)value;
            if (age < 1)
            {
                return false;
                throw new DbEntityValidationException("Invalid Age");
            }
            else
            {
                return true;
            }           
        }
    }
}
