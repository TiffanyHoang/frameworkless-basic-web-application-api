using System;
using Xunit;
using System.Net.Http;

namespace WebApplication_E2ETest
{
    public class E2ETest 
    {
        private readonly HttpClient _client;
        private string _endpoint;
        public E2ETest()
        {
            _client = new HttpClient();
            _endpoint = Environment.GetEnvironmentVariable("Endpoint") ?? "https://tiffany.fma.lab.myobdev.com";
        }

        [Fact]
        public void GETRoot_ReturnGreetingMessageWithPeopleInTheList()
        {
            var response = _client.GetAsync($"{_endpoint}");

            var responseString = response.Result.Content.ReadAsStringAsync().Result;

            var time = DateTime.Now.ToString("HH:mm");
            var date = DateTime.Now.ToString("dd MMM yyyy");
            var expectedString = $"Hello Tiffany - The time on the server is {time} on {date}";

            Assert.Equal(expectedString, responseString);
        }

        [Fact]
        public void GETPeople_ReturnPeopleList()
        {
            var response = _client.GetAsync($"{_endpoint}/people");

            var responseString = response.Result.Content.ReadAsStringAsync().Result;
            
            var expectedString = "[\n  {\n    \"Name\": \"Tiffany\"\n  }\n]";

            Assert.Equal(expectedString, responseString);
        }

        // [Fact]
        // public void POSTPeople_ReturnNewPersonAndAddNewPersonInPeopleList()
        // {
        //     var values = new Dictionary<string, string>
        //         {
        //             { "Name", "DS" },
        //         };
        //     string json = JsonConvert.SerializeObject(values, Formatting.Indented);
        //     var content = new StringContent(json);
        //     var response = _client.PostAsync($"{_endpoint}/people", content);

        //     _server.ProcessRequest();

        //     var responseString = response.Result.Content.ReadAsStringAsync().Result;

        //     Assert.Equal("{\n  \"Name\": \"DS\"\n}", responseString);

        //     response = _client.GetAsync($"{_endpoint}/people");

        //     _server.ProcessRequest();

        //     responseString = response.Result.Content.ReadAsStringAsync().Result;
        //     var expectedString = "[\n  {\n    \"Name\": \"Tiffany\"\n  },\n  {\n    \"Name\": \"DS\"\n  }\n]";

        //     Assert.Equal(expectedString, responseString);
        // }

        // [Fact]
        // public void PUTPeople_ReturnUpdatedPersonAndUpdatePersonInPeopleList()
        // {
        //     var values = new Dictionary<string, string>
        //         {
        //             { "Name", "DS" },
        //         };
        //     string json = JsonConvert.SerializeObject(values, Formatting.Indented);
        //     var content = new StringContent(json);
        //     var response = _client.PostAsync($"{_endpoint}/people", content);

        //     _server.ProcessRequest();

        //     var responseString = response.Result.Content.ReadAsStringAsync().Result;

        //     Assert.Equal("{\n  \"Name\": \"DS\"\n}", responseString);

        //     values = new Dictionary<string, string>
        //         {
        //             { "Name", "DSTeoh" },
        //         };
        //     json = JsonConvert.SerializeObject(values, Formatting.Indented);
        //     content = new StringContent(json);
        //     response = _client.PutAsync($"{_endpoint}/people/DS", content);

        //     _server.ProcessRequest();

        //     responseString = response.Result.Content.ReadAsStringAsync().Result;

        //     Assert.Equal("{\n  \"Name\": \"DSTeoh\"\n}", responseString);

        //     response = _client.GetAsync($"{_endpoint}/people");

        //     _server.ProcessRequest();

        //     responseString = response.Result.Content.ReadAsStringAsync().Result;
        //     var expectedString = "[\n  {\n    \"Name\": \"Tiffany\"\n  },\n  {\n    \"Name\": \"DSTeoh\"\n  }\n]";

        //     Assert.Equal(expectedString, responseString);
        // }

        // [Fact]
        // public void DELETEPeople_DeletePersonInPeopleList()
        // {
        //     var values = new Dictionary<string, string>
        //         {
        //             { "Name", "DS" },
        //         };
        //     string json = JsonConvert.SerializeObject(values, Formatting.Indented);
        //     var content = new StringContent(json);
        //     var response = _client.PostAsync($"{_endpoint}/people", content);

        //     _server.ProcessRequest();

        //     var responseString = response.Result.Content.ReadAsStringAsync().Result;

        //     Assert.Equal("{\n  \"Name\": \"DS\"\n}", responseString);

        //     response = _client.GetAsync($"{_endpoint}/people");

        //     _server.ProcessRequest();

        //     responseString = response.Result.Content.ReadAsStringAsync().Result;
            
        //     var expectedString = "[\n  {\n    \"Name\": \"Tiffany\"\n  },\n  {\n    \"Name\": \"DS\"\n  }\n]";
            
        //     Assert.Equal(expectedString, responseString);

        //     json = JsonConvert.SerializeObject(values, Formatting.Indented);
        //     content = new StringContent(json);
        //     response = _client.DeleteAsync($"{_endpoint}/people/DS");

        //     _server.ProcessRequest();

        //     response = _client.GetAsync($"{_endpoint}/people");

        //     _server.ProcessRequest();

        //     responseString = response.Result.Content.ReadAsStringAsync().Result;
            
        //     expectedString = "[\n  {\n    \"Name\": \"Tiffany\"\n  }\n]";

        //     Assert.Equal(expectedString, responseString);
        // }
    }
}