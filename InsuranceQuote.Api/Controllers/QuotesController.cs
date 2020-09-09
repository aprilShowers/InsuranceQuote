using AutoMapper;
using InsuranceQuote.Api.Data;
using InsuranceQuote.Api.Data.Entities;
using InsuranceQuote.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<QuotesController> _logger;
        private readonly IMapper _mapper;

        RatingEngine ratingEngine = new RatingEngine();

        public QuotesController(IQuoteRepo repo, ILogger<QuotesController> logger, IMapper mapper)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
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
            _logger.LogInformation($"GetCustomerById was called with Id: {id}");

            var customer = _repo.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(customer);
            }
        }

        [HttpPost]
        public IActionResult AddNewCustomer([FromBody] CustomerCreateDto newCustomer)
        {
            // TODO: revisit -because Revenue is a decimal, if no Revenue sent in payload,
            // default value gets stored as 0.00 in db and premium is not calculated (0)
            // options: make nullable in db (seems dirty), change type in db or use rangeattribute ??
            // so meaningful errors can be logged on the BadReq resp 

            if (newCustomer == null || newCustomer.Revenue == 0.0m) // <- quick and dirty fix for now
            {
                return BadRequest();
            }
            else
            {
                var newCustomerModel = _mapper.Map<InsuranceCustomer>(newCustomer);
                var calculatedPremium = ratingEngine.Rate(newCustomerModel);
                _logger.LogInformation($"RatingEngine returned unrounded premium of: {calculatedPremium}");

                var roundedPremium = Math.Round(calculatedPremium, 2);
                newCustomerModel.Premium = roundedPremium;
                _logger.LogInformation($"Rounded premium is: {roundedPremium}");

                _repo.AddCustomer(newCustomerModel);
                _repo.Save();

                var quoteReadDTO = _mapper.Map<CustomerQuoteReadDto>(newCustomerModel);

                return CreatedAtRoute(nameof(GetCustomerById), new { id = newCustomerModel.Id }, quoteReadDTO);
            }
        }


    }
}
