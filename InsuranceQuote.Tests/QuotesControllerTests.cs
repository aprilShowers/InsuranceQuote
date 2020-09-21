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
            var expected = "{\"id\":3,\"revenue\":45000.00,\"state\":\"OH\",\"business\":\"Architect\",\"premium\":0.00}";

            var result = await _client.GetStringAsync("http://localhost/api/quotes/3");

            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task GetCustomerInvalidId_ReturnsNotFound()
        {
            var expected = 404;

            var result = await _client.GetAsync("http://localhost/api/quotes/88");

            Assert.Contains(expected.ToString(), result.ToString());
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

        [Fact]
        public async Task Post_WithLargestAllowedRevenue_ReturnsCreated()
        {
            var customer = new TestCustomerCreateModel
            {
                Revenue = 8888888888888888.88m,
                State = "FL",
                Business = "Plumber"
            };

            var serializeCustomer = JsonConvert.SerializeObject(customer);
            var content = new StringContent(serializeCustomer);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _client.PostAsync("", content);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task Post_WithLargerThan_AllowedRevenue_ReturnsBadRequest()
        {
            var customer = new TestCustomerCreateModel
            {
                Revenue = 88888888888888885.88m,
                State = "FL",
                Business = "Plumber"
            };

            var serializeCustomer = JsonConvert.SerializeObject(customer);
            var content = new StringContent(serializeCustomer);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _client.PostAsync("", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Post_WithMissingState_ReturnsBadRequest()
        {
            var invalidCustomer = new TestCustomerCreateModel
            {
                Revenue = 15000,
                Business = "Plumber"
            };

            var serializeCustomer = JsonConvert.SerializeObject(invalidCustomer);
            var content = new StringContent(serializeCustomer);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _client.PostAsync("", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Post_WithInvalidState_ReturnsBadRequest()
        {
            var invalidCustomer = new TestCustomerCreateModel
            {
                State = "MA",
                Revenue = 15000,
                Business = "Plumber"
            };

            var serializeCustomer = JsonConvert.SerializeObject(invalidCustomer);
            var content = new StringContent(serializeCustomer);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _client.PostAsync("", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Post_WithInvalidBusiness_ReturnsBadRequest()
        {
            var invalidCustomer = new TestCustomerCreateModel
            {
                State = "OH",
                Revenue = 6565000,
                Business = "Other"
            };

            var serializeCustomer = JsonConvert.SerializeObject(invalidCustomer);
            var content = new StringContent(serializeCustomer);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _client.PostAsync("", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Post_WithInvalidRevenue_ReturnsBadRequest()
        {
            var invalidCustomer = new TestCustomerCreateModel
            {
                State = "TX",
                Revenue = 0.5m,
                Business = "Plumber"
            };

            var serializeCustomer = JsonConvert.SerializeObject(invalidCustomer);
            var content = new StringContent(serializeCustomer);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _client.PostAsync("", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Put_ReturnsNoContent()
        {
            var customer = new TestCustomerCreateModel
            {
                Revenue = 15000,
                State = "FL",
                Business = "Plumber"
            };

            var serializeUpdatedCustomer = JsonConvert.SerializeObject(customer);
            var stringContent = new StringContent(serializeUpdatedCustomer, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("http://localhost/api/quotes/1", stringContent);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task Put_InvalidId_ReturnsNotFound()
        {
            var customer = new TestCustomerCreateModel
            {
                Revenue = 222222,
                State = "TX",
                Business = "Plumber"
            };

            var serializeUpdatedCustomer = JsonConvert.SerializeObject(customer);
            var stringContent = new StringContent(serializeUpdatedCustomer, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("http://localhost/api/quotes/99", stringContent);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Put_WithMissingRevenue_ReturnsBadRequest()
        {
            var customer = new TestCustomerCreateModel
            {
                State = "FL",
                Business = "Programmer"
            };

            var serializeUpdatedCustomer = JsonConvert.SerializeObject(customer);
            var stringContent = new StringContent(serializeUpdatedCustomer, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("http://localhost/api/quotes/2", stringContent);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Put_WithInvalidState_ReturnsBadRequest()
        {
            var customer = new TestCustomerCreateModel
            {
                Revenue = 383838.00m,
                State = "PA",
                Business = "Programmer"
            };

            var serializeUpdatedCustomer = JsonConvert.SerializeObject(customer);
            var stringContent = new StringContent(serializeUpdatedCustomer, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("http://localhost/api/quotes/2", stringContent);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent()
        {
            var response = await _client.DeleteAsync("http://localhost/api/quotes/5");

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task Delete_InvalidId_ReturnsNotFound()
        {
            var response = await _client.DeleteAsync("http://localhost/api/quotes/777");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
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
