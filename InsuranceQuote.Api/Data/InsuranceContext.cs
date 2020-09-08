using InsuranceQuote.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceQuote.Api.Data
{
    public class InsuranceContext : DbContext
    {
        public InsuranceContext(DbContextOptions<InsuranceContext> opt) : base(opt)
        {
        }

        DbSet<InsuranceCustomer> InsuranceCustomers { get; set; }
    }
}
