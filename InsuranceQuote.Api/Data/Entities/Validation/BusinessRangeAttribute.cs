using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceQuote.Api.Data.Entities.Validation
{
    public class BusinessRangeAttribute : ValidationAttribute
    {
        public string[] AllowedProfessions { get; set; }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (AllowedProfessions?.Contains(value?.ToString()) == true)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult($"Insurance offered for these Professions: {string.Join(", ", AllowedProfessions)}");
        }
    }
}
