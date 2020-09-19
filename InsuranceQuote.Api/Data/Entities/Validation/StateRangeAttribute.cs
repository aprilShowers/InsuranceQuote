using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceQuote.Api.Data.Entities.Validation
{
    public class StateRangeAttribute : ValidationAttribute
    {
        public string[] AllowedStates { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (AllowedStates?.Contains(value?.ToString()) == true)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult($"Insurance offered in these States: {string.Join(", ", AllowedStates)}");
        }
    }
}
