using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InsuranceQuote.Tests.Models
{
    class TestCustomerCreateModel
    {
        public int Id { get; set; }

        public decimal Revenue { get; set; }

        public string State { get; set; }

        public string Business { get; set; }
    }
}
