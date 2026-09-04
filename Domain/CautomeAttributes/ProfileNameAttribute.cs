using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.CautomeAttributes
{
    internal class ProfileNameAttribute:ValidationAttribute
    {
        protected override ValidationResult? IsValid(
       object? value,
       ValidationContext validationContext)
        {
            if (value is not string userName)
                return new ValidationResult("Invalid username.");

            if (!Regex.IsMatch(userName, @"^[A-Za-z0-9]+$")  )
                return new ValidationResult(
                    "Username can contain only letters and numbers.");
            if( userName.Length < 2)
                return new ValidationResult(
                   "Username is too short");

            return ValidationResult.Success;
        }
    }
}
