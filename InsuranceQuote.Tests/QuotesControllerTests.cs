using InsuranceQuote.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace InsuranceQuote.Tests
{
    public class QuotesControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public QuotesControllerTests(WebApplicationFactory<Startup> factory)
        {
            factory.ClientOptions.BaseAddress = new Uri("http://localhost/api/quotes");
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetCustomers_ReturnsSuccessStatusCode()
        {
            var result = await _client.GetAsync("");

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }


    }
}
