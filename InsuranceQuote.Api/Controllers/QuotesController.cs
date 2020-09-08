using InsuranceQuote.Api.Data;
using InsuranceQuote.Api.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceQuote.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class QuotesController : ControllerBase
    {
        private readonly IQuoteRepo _repo;

        public QuotesController(IQuoteRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<InsuranceCustomer>> GetCustomers()
        {
            var customers = _repo.GetAllCustomers();
            return Ok(customers);
        }

        [HttpGet("{id:int}")]
        public ActionResult<InsuranceCustomer> GetCustomerUsingId(int id)
        {
            var customer = _repo.GetCustomerById(id);
            return Ok(customer);
        }

    }
}
