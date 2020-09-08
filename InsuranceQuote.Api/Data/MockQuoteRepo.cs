using InsuranceQuote.Api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceQuote.Api.Data
{
    public class MockQuoteRepo : IQuoteRepo
    {
        public IEnumerable<InsuranceCustomer> GetAllCustomers()
        {
            var allCustomers = new List<InsuranceCustomer>
            {
                new InsuranceCustomer { Id = 1, Revenue = 5000.00m, State = "OH", Business = "Architect" },
                new InsuranceCustomer { Id = 2, Revenue = 65000.00m, State = "FL", Business = "Plumber" },
                new InsuranceCustomer { Id = 3, Revenue = 890000.00m, State = "TX", Business = "Programmer" },
            };
            return allCustomers;
        }

        public InsuranceCustomer GetCustomerById(int id)
        {
            return new InsuranceCustomer { Id = 1, Revenue = 65000.00m, State = "FL", Business = "Plumber" };
        }
    }
}
