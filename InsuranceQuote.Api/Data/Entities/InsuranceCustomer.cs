using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceQuote.Api.Data.Entities
{
    public class InsuranceCustomer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Revenue { get; set; }

        [Required]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Please enter the 2 character state abbreviation")]
        public string State { get; set; }

        [Required]
        [MaxLength(40)]
        public string Business { get; set; }

        public decimal Premium { get; set; }
    }
}
