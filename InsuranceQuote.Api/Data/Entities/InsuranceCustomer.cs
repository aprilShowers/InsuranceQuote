﻿using InsuranceQuote.Api.Data.Entities.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceQuote.Api.Data.Entities
{
    public class InsuranceCustomer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(1.00, 9999999999999999.99, ErrorMessage = "Please enter a Revenue between 1 and 16 Digits")]
        public decimal Revenue { get; set; }

        [Required]
        [StateRange(AllowedStates = new[] { "FL", "OH", "TX" })]
        [StringLength(2, MinimumLength = 2)]
        public string State { get; set; }

        [Required]
        [BusinessRange(AllowedProfessions = new[] { "Architect", "Plumber", "Programmer" })]
        [MaxLength(40)]
        public string Business { get; set; }

        public decimal Premium { get; set; }
    }
}
