using InsuranceQuote.Api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceQuote.Api.Data
{
    public interface IQuoteRepo
    {
        IEnumerable<InsuranceCustomer> GetAllCustomers();

        InsuranceCustomer GetCustomerById(int id);

        void AddCustomer(InsuranceCustomer insuranceCustomer);

        bool Save();
    }
}
