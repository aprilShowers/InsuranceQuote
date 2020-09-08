using InsuranceQuote.Api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceQuote.Api.Data
{
    public class QuoteRepo : IQuoteRepo
    {
        private readonly InsuranceContext _context;

        public QuoteRepo(InsuranceContext context)
        {
            _context = context;
        }
        public IEnumerable<InsuranceCustomer> GetAllCustomers()
        {
            return _context.InsuranceCustomers.ToList();
        }

        public InsuranceCustomer GetCustomerById(int id)
        {
            return _context.InsuranceCustomers.Where(c => c.Id == id).FirstOrDefault();
        }
    }
}
