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

        public DbSet<InsuranceCustomer> InsuranceCustomers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<InsuranceCustomer>()
                .HasData(
                new InsuranceCustomer()
                {
                    Id = 1,
                    Revenue = 8200.00m,
                    State = "FL",
                    Business = "Plumber"
                },
                new InsuranceCustomer()
                {
                    Id = 2,
                    Revenue = 20000.00m,
                    State = "FL",
                    Business = "Programmer"
                },
                new InsuranceCustomer()
                {
                    Id = 3,
                    Revenue = 45000.00m,
                    State = "OH",
                    Business = "Architect"
                },
                new InsuranceCustomer()
                {
                    Id = 4,
                    Revenue = 45000.00m,
                    State = "TX",
                    Business = "Architect"
                },
                new InsuranceCustomer()
                {
                    Id = 5,
                    Revenue = 1000000.00m,
                    State = "OH",
                    Business = "Programmer"
                });
        }
    }
}
