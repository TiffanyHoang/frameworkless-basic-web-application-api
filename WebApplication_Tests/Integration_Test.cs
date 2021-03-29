using Xunit;
using System;
using System.Net.Http;
using System.Net;
using WebApplication;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication_Tests
{
    public class Integration_Test
    {
        private readonly HttpClient _client;
        private readonly Server _server;
        public Integration_Test()
        {
            _server = new Server();
            _client = new HttpClient();
        }

        [Fact]
        public void GETPeople_ReturnPeopleList()
        {
            var response = _client.GetAsync("http://localhost:8080/people");
            _server.ProcessRequest();
            var responseString = response.Result.Content.ReadAsStringAsync().Result;
            var expectedString = "[\n  {\n    \"Name\": \"Tiffany\"\n  }\n]";

            Assert.Equal(expectedString, responseString);

            var values = new Dictionary<string, string>
                {
                    { "Name", "DS" },
                };
            string json = JsonConvert.SerializeObject(values, Formatting.Indented);
            var content = new StringContent(json);
            response = _client.PostAsync("http://localhost:8080/people", content);
            _server.ProcessRequest();
            responseString = response.Result.Content.ReadAsStringAsync().Result;

            Assert.Equal("{\n  \"Name\": \"DS\"\n}", responseString);
        }
    }
}
