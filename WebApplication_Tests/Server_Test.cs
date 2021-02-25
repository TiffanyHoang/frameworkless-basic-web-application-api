using Xunit;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace WebApplication_Tests
{
    public class Server_Test
    {
        private readonly HttpClient client;

        public Server_Test()
        {
            client = new HttpClient();
        }


        [Fact]
        public async void testGET()
        {
            HttpClient client = new HttpClient();
            var responseString = await client.GetStringAsync("http://localhost:8080/people");
            var expectedString = @"[\n  {\n    ""Name"": ""Bob""\n  },\n  {\n    ""Name"": ""Bob""\n  }\n]";
            Assert.Equal(expectedString, responseString);
        }

        [Fact]
        public async void testPost()
        {
            var values = new Dictionary<string, string>
                {
                    { "Name", "DS" },
                };

            string json = JsonConvert.SerializeObject(values, Formatting.Indented);
            var content = new StringContent(json);
            var response = await client.PostAsync("http://localhost:8080/people", content);

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal("Created DS", responseString);
        }
    }
}

