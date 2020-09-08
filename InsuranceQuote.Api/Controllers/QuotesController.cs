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
        RatingEngine ratingEngine = new RatingEngine();

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

        [HttpGet("{id:int}", Name = "GetCustomerById")]
        public ActionResult<InsuranceCustomer> GetCustomerById(int id)
        {
            var customer = _repo.GetCustomerById(id);
            return Ok(customer);
        }

        [HttpPost]
        public IActionResult AddNewCustomer([FromBody] InsuranceCustomer newCustomerInfo)
        {
            // call ratingEngine.Rate() which sets the Premium decimal on obj
            // call AddCustomer in repo with newCustomerInfo

            var calculatedPremium = ratingEngine.Rate(newCustomerInfo);
            newCustomerInfo.Premium = calculatedPremium;

            _repo.AddCustomer(newCustomerInfo);
            _repo.Save();

            // post returns a 201 but need to map to a ResponseDTO to return just premium to client
            // premium on ResponseDTO needs to be rounded up to an int
            // also, round the Premium stored on InsuranceCustomer 
            // the resp contains ( "premium": 2328.3933620 ) -no bueno

            return CreatedAtRoute(nameof(GetCustomerById), new { id = newCustomerInfo.Id }, newCustomerInfo);
        }

    }
}
