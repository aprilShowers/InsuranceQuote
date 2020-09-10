using InsuranceQuote.Api;
using InsuranceQuote.Api.Data.Entities;
using InsuranceQuote.Tests.Mocks;
using InsuranceQuote.Tests.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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

        [Fact]
        public async Task GetCustomers_ReturnsContent()
        {
            var result = await _client.GetAsync("");

            Assert.NotNull(result.Content);
            Assert.True(result.Content.Headers.ContentLength > 0);
        }

        [Fact]
        public async Task GetCustomerById_ReturnsExpectedJson()
        {
            var expected = "{\"id\":1,\"revenue\":8200.00,\"state\":\"FL\",\"business\":\"Plumber\",\"premium\":0.00}";
            
            var result = await _client.GetStringAsync("");

            Assert.Contains(expected, result);
        }

        [Fact]
        public async Task Post_WithValidModel_ReturnsCreated()
        {
            var newCustomer = MakeCustomerCreateModel();
            var serializeCustomer = JsonConvert.SerializeObject(newCustomer);
            var content = new StringContent(serializeCustomer);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            
            var response = await _client.PostAsync("", content);
            
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        private static TestCustomerCreateModel MakeCustomerCreateModel() 
        {
            return new TestCustomerCreateModel
            {
                Revenue = 1234567,
                State = "OH",
                Business = "Architect"
            };
        }

    }
}
