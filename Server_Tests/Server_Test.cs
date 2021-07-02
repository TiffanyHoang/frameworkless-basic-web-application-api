using Xunit;
using System;
using System.Net.Http;
using WebApplication;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace Server_Tests
{
    public class Server_Test : IDisposable
    {
        private readonly HttpClient _client;
        private Server _server;
        private string _endpoint;
        public Server_Test()
        {
            var secret = Environment.GetEnvironmentVariable("SECRET");
            _endpoint = Environment.GetEnvironmentVariable("ENDPOINT") ?? "http://localhost:8080";
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Authorization
                         = new AuthenticationHeaderValue("Basic", secret);
            if (_endpoint == "http://localhost:8080")
            {
                _server = new Server();
                _server.Start();
            }
        }

        public void Dispose()
        {
            if (_endpoint == "http://localhost:8080")
            {
                _server.Close();
            }
        }

        [Fact]
        public async Task E2E_Test()
        {
            // Greeting
            var response = await _client.GetAsync(_endpoint);
            var responseString = response.Content.ReadAsStringAsync().Result;
            var time = DateTime.Now.ToUniversalTime().ToString("HH:mm");
            var date = DateTime.Now.ToUniversalTime().ToString("dd MMM yyyy");
            var expectedString = $"Hello Tiffany - The time on the server is {time} on {date}";
            Assert.Equal(expectedString, responseString);

            // Get people
            response = await _client.GetAsync(_endpoint + "/people");
            responseString = response.Content.ReadAsStringAsync().Result;
            expectedString = "[\n  {\n    \"Name\": \"Tiffany\"\n  }\n]";
            Assert.Equal(expectedString, responseString);

            // Add new person
            var values = new Dictionary<string, string>
                {
                    { "Name", "DS" },
                };
            string json = JsonConvert.SerializeObject(values, Formatting.Indented);
            var content = new StringContent(json);
            await _client.PostAsync(_endpoint + "/people", content);
            response = await _client.GetAsync(_endpoint + "/people");
            responseString = response.Content.ReadAsStringAsync().Result;
            expectedString = "[\n  {\n    \"Name\": \"Tiffany\"\n  },\n  {\n    \"Name\": \"DS\"\n  }\n]";
            Assert.Equal(expectedString, responseString);

            // Update person
            values = new Dictionary<string, string>
                {
                    { "Name", "DSTeoh" },
                };
            json = JsonConvert.SerializeObject(values, Formatting.Indented);
            content = new StringContent(json);
            await _client.PutAsync(_endpoint + "/people/DS", content);
            response = await _client.GetAsync(_endpoint + "/people");
            responseString = response.Content.ReadAsStringAsync().Result;
            expectedString = "[\n  {\n    \"Name\": \"Tiffany\"\n  },\n  {\n    \"Name\": \"DSTeoh\"\n  }\n]";
            Assert.Equal(expectedString, responseString);

            // Delete person
            await _client.DeleteAsync(_endpoint + "/people/DSTeoh");
            response = await _client.GetAsync(_endpoint + "/people");
            responseString = response.Content.ReadAsStringAsync().Result;
            expectedString = "[\n  {\n    \"Name\": \"Tiffany\"\n  }\n]";
            Assert.Equal(expectedString, responseString);
        }
    }
}
