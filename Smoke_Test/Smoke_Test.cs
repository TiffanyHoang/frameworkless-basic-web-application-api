using System;
using Xunit;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Smoke_Test
{
    public class Smoke_Test
    {
        private readonly HttpClient _client;
        private string _endpoint;

        public Smoke_Test()
        {
            var secret = Environment.GetEnvironmentVariable("SECRET");
            _endpoint = Environment.GetEnvironmentVariable("ENDPOINT");
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Authorization 
                         = new AuthenticationHeaderValue("Basic", secret);
        }

        [Fact]
        public async Task TestProductionGreetingEndpoint()
        {
            var response = await _client.GetAsync(_endpoint);
            var responseStatus = response.StatusCode;
            Assert.Equal(HttpStatusCode.OK, responseStatus);
        }

        [Fact]
        public async Task TestProductionPeopleEndpoint()
        {
            var response = await _client.GetAsync(_endpoint + "/people");
            var responseStatus = response.StatusCode;
            Assert.Equal(HttpStatusCode.OK, responseStatus);
        }
    }
}
