using Xunit;
using System;
using System.Net.Http;
using WebApplication;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WebApplication_Tests
{
    public class Server_Test : IDisposable
    {
        private readonly HttpClient _client;
        private readonly Server _server;
        public Server_Test()
        {
            _server = new Server();
            _client = new HttpClient();
        }

        public void Dispose()
        {
            _server.Close();
        }


        [Fact]
        public void GETRoot_ReturnGreetingMessageWithPeopleInTheList()
        {
            var values = new Dictionary<string, string>
                {
                    { "Name", "DS" },
                };
            string json = JsonConvert.SerializeObject(values, Formatting.Indented);
            var content = new StringContent(json);
            var response = _client.PostAsync("http://localhost:8080/people", content);

            _server.ProcessRequest();

            var responseString = response.Result.Content.ReadAsStringAsync().Result;

            response = _client.GetAsync("http://localhost:8080/people");

            _server.ProcessRequest();

            responseString = response.Result.Content.ReadAsStringAsync().Result;

            var expectedString = "[\n  {\n    \"Name\": \"Tiffany\"\n  },\n  {\n    \"Name\": \"DS\"\n  }\n]";

            Assert.Equal(expectedString, responseString);
        
            response = _client.GetAsync("http://localhost:8080/");

            _server.ProcessRequest();

            responseString = response.Result.Content.ReadAsStringAsync().Result;

            var time = DateTime.Now.ToUniversalTime().ToString("HH:mm");
            var date = DateTime.Now.ToUniversalTime().ToString("dd MMM yyyy");
            expectedString = $"Hello Tiffany DS - The time on the server is {time} on {date}";

            Assert.Equal(expectedString, responseString);
        }

        [Fact]
        public void GETPeople_ReturnPeopleList()
        {
            var response = _client.GetAsync("http://localhost:8080/people");

            _server.ProcessRequest();

            var responseString = response.Result.Content.ReadAsStringAsync().Result;
            
            var expectedString = "[\n  {\n    \"Name\": \"Tiffany\"\n  }\n]";

            Assert.Equal(expectedString, responseString);
        }

        [Fact]
        public void POSTPeople_ReturnNewPersonAndAddNewPersonInPeopleList()
        {
            var values = new Dictionary<string, string>
                {
                    { "Name", "DS" },
                };
            string json = JsonConvert.SerializeObject(values, Formatting.Indented);
            var content = new StringContent(json);
            var response = _client.PostAsync("http://localhost:8080/people", content);

            _server.ProcessRequest();

            var responseString = response.Result.Content.ReadAsStringAsync().Result;

            Assert.Equal("{\n  \"Name\": \"DS\"\n}", responseString);

            response = _client.GetAsync("http://localhost:8080/people");

            _server.ProcessRequest();

            responseString = response.Result.Content.ReadAsStringAsync().Result;
            var expectedString = "[\n  {\n    \"Name\": \"Tiffany\"\n  },\n  {\n    \"Name\": \"DS\"\n  }\n]";

            Assert.Equal(expectedString, responseString);
        }

        [Fact]
        public void PUTPeople_ReturnUpdatedPersonAndUpdatePersonInPeopleList()
        {
            var values = new Dictionary<string, string>
                {
                    { "Name", "DS" },
                };
            string json = JsonConvert.SerializeObject(values, Formatting.Indented);
            var content = new StringContent(json);
            var response = _client.PostAsync("http://localhost:8080/people", content);

            _server.ProcessRequest();

            var responseString = response.Result.Content.ReadAsStringAsync().Result;

            Assert.Equal("{\n  \"Name\": \"DS\"\n}", responseString);

            values = new Dictionary<string, string>
                {
                    { "Name", "DSTeoh" },
                };
            json = JsonConvert.SerializeObject(values, Formatting.Indented);
            content = new StringContent(json);
            response = _client.PutAsync("http://localhost:8080/people/DS", content);

            _server.ProcessRequest();

            responseString = response.Result.Content.ReadAsStringAsync().Result;

            Assert.Equal("{\n  \"Name\": \"DSTeoh\"\n}", responseString);

            response = _client.GetAsync("http://localhost:8080/people");

            _server.ProcessRequest();

            responseString = response.Result.Content.ReadAsStringAsync().Result;
            var expectedString = "[\n  {\n    \"Name\": \"Tiffany\"\n  },\n  {\n    \"Name\": \"DSTeoh\"\n  }\n]";

            Assert.Equal(expectedString, responseString);
        }

        [Fact]
        public void DELETEPeople_DeletePersonInPeopleList()
        {
            var values = new Dictionary<string, string>
                {
                    { "Name", "DS" },
                };
            string json = JsonConvert.SerializeObject(values, Formatting.Indented);
            var content = new StringContent(json);
            var response = _client.PostAsync("http://localhost:8080/people", content);

            _server.ProcessRequest();

            var responseString = response.Result.Content.ReadAsStringAsync().Result;

            Assert.Equal("{\n  \"Name\": \"DS\"\n}", responseString);

            response = _client.GetAsync("http://localhost:8080/people");

            _server.ProcessRequest();

            responseString = response.Result.Content.ReadAsStringAsync().Result;
            
            var expectedString = "[\n  {\n    \"Name\": \"Tiffany\"\n  },\n  {\n    \"Name\": \"DS\"\n  }\n]";
            
            Assert.Equal(expectedString, responseString);

            json = JsonConvert.SerializeObject(values, Formatting.Indented);
            content = new StringContent(json);
            response = _client.DeleteAsync("http://localhost:8080/people/DS");

            _server.ProcessRequest();

            response = _client.GetAsync("http://localhost:8080/people");

            _server.ProcessRequest();

            responseString = response.Result.Content.ReadAsStringAsync().Result;
            
            expectedString = "[\n  {\n    \"Name\": \"Tiffany\"\n  }\n]";

            Assert.Equal(expectedString, responseString);
        }
    }
}
