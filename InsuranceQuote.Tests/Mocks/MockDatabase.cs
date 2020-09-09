using InsuranceQuote.Api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceQuote.Tests.Mocks
{
    public class MockDatabase
    {
       
        public MockDatabase()
        {
            GetDefaultCustomers();
        }

        /*public static MockDatabase WithDefaultCustomers()
        {
            var database = new MockDatabase();
            database.GetDefaultCustomers();
            return database;
        }*/

        public List<InsuranceCustomer> GetDefaultCustomers() => new List<InsuranceCustomer>
            {
                new InsuranceCustomer { Id = 1, Revenue = 15500.00m, State = "FL", Business = "Plumber", Premium = 37.20m},
                new InsuranceCustomer { Id = 3, Revenue = 158900.00m, State = "TX", Business = "Architect", Premium = 599.37m }
            };
    }
}
