using AutoMapper;
using InsuranceQuote.Api.Data;
using InsuranceQuote.Api.Data.Entities;
using InsuranceQuote.Api.Dtos;
using Microsoft.AspNetCore.JsonPatch;
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
            if (newCustomer == null)
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

                newCustomerModel.Premium = Math.Round(newCustomerModel.Premium, MidpointRounding.ToEven);
                var quoteReadDTO = _mapper.Map<CustomerQuoteReadDto>(newCustomerModel);
                
                return CreatedAtRoute(nameof(GetCustomerById), new { id = newCustomerModel.Id }, quoteReadDTO);
            }
        }

        // to Patch pass this payload: -can pass multiple in replace objects into json array (comma delimited)
        /*[
            {
                "op": "replace",
                "path": "/revenue",
                "value": "15000"
            }
        ]*/

        [HttpPatch("{id:int}")]
        public ActionResult PartialCustomerUpdate(int id, JsonPatchDocument<CustomerUpdateDto> patchDoc)
        {
            var customerModel = _repo.GetCustomerById(id);
            if (customerModel == null)
            {
                return NotFound();
            }

            var insuranceCustomerToPatch = _mapper.Map<CustomerUpdateDto>(customerModel);
            patchDoc.ApplyTo(insuranceCustomerToPatch, ModelState);

            if (!TryValidateModel(insuranceCustomerToPatch))
            {
                return ValidationProblem(ModelState);
            }
            
            _mapper.Map(insuranceCustomerToPatch, customerModel);

            var calculatedPremium = ratingEngine.Rate(customerModel);
            _logger.LogInformation($"RatingEngine returned unrounded premium of: {calculatedPremium}");
            var roundedPremium = Math.Round(calculatedPremium, 2);
            customerModel.Premium = roundedPremium;
            _logger.LogInformation($"Rounded premium is: {roundedPremium}");

            _repo.Save();

            return NoContent();
        }

        [HttpPut("{id:int}")]
        public ActionResult UpdateCustomerInfo(int id, [FromBody] CustomerUpdateDto updatedCustomer)
        {
            var customerModel = _repo.GetCustomerById(id);
            if (customerModel == null)
            {
                return NotFound();
            }

            updatedCustomer.Id = customerModel.Id;
            var updatedCustomerModel = _mapper.Map(updatedCustomer, customerModel);

            var calculatedPremium = ratingEngine.Rate(updatedCustomerModel);
            _logger.LogInformation($"RatingEngine returned unrounded premium of: {calculatedPremium}");

            var roundedPremium = Math.Round(calculatedPremium, 2);
           
            updatedCustomerModel.Premium = roundedPremium;
            _logger.LogInformation($"Rounded premium is: {roundedPremium}");

            _repo.Save();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public ActionResult DeleteCustomer(int id)
        {
            var customerModel = _repo.GetCustomerById(id);
            if (customerModel == null)
            {
                return NotFound();
            }
            _repo.DeleteCustomer(customerModel);
            _repo.Save();
            return NoContent();
        }
    }
}
