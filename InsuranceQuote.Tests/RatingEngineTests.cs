using InsuranceQuote.Api;
using InsuranceQuote.Api.Data.Entities;
using System;
using Xunit;

namespace InsuranceQuote.Tests
{
    public class RatingEngineTests
    {
        private readonly RatingEngine _ratingEngine; 
        public RatingEngineTests()
        {
            _ratingEngine = new RatingEngine();
        }

        [Fact]
        public void Rate_ReturnsCorrectPremium()
        {
            var testCustomer = new InsuranceCustomer
            { Id = 1, Revenue = 158900.00m, State = "TX", Business = "Architect" };

            var expectedPremium = Math.Round(_ratingEngine.Rate(testCustomer), 2).ToString();

            Assert.Equal(599.37.ToString(), expectedPremium);
        }
    }
}
