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
        // This throws a 500 if state not passed in payload - I think 400 with more detail is better 

        /*protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value.ToString() == "FL" || value.ToString() == "OH" || value.ToString() == "TX")
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult($"Insurance offered in these States: {string.Join(", ", AllowedStates)}");
            }
        }*/

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
