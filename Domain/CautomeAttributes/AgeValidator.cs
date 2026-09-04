using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CautomeAttributes
{
    internal sealed class AgeValidator:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value is not DateOnly dateOfBirth)
                return new ValidationResult("Invalid date of birth");


            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            var diffYears = today.Year - dateOfBirth.Year;

            if (dateOfBirth > today.AddYears(-diffYears))
                diffYears--;


           return  diffYears >= 15 ? null : new ValidationResult("you're still young");
              
                
            
        }

    }
}
