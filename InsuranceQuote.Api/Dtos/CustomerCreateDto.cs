using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceQuote.Api.Dtos
{
    public class CustomerCreateDto
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(1.00, 9999999999999999.99, ErrorMessage = "Please enter a Revenue greater than 1")]
        public decimal Revenue { get; set; }

        [Required]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Please enter the 2 character state abbreviation")]
        public string State { get; set; }

        [Required]
        [MaxLength(40)]
        public string Business { get; set; }
    }
}
