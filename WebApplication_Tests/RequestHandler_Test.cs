using System;
using Xunit;
using System.Net;
using WebApplication;
using System.Net.Http;
using WebApplication_Tests.Server_Test;
using WebApplication.Server;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WebApplication_Tests
{
    public class RequestHandler_Test
    {
        private readonly Repository _repository;
        private readonly RequestHandler _requestHandler;

        private HttpClient _client;
        public RequestHandler_Test()
        {
            _repository = new Repository();
            _requestHandler = new RequestHandler(_repository);
            _client = new HttpClient();
        }


        [Fact]
        public void Greeting_ReturnCorrectGreetingMessage()
        {
            var responseString = _requestHandler.Greeting();
            var time = DateTime.Now.ToString("HH:mm");
            var date = DateTime.Now.ToString("dd MMM yyyy");
            var expected = $"Hello Tiffany - the time on the server is {time} on {date}";

            Assert.Equal(expected, responseString);
        }

        [Fact]
        public async void GetPeopleList_ReturnCorrectListofPeopleInJsonFormat()
        {
            IServer mockServer = new MockServer("http://*:8081/people");
            var context = mockServer.Context();
            var person = new Person("DS");
            var values = new Dictionary<string, string>
                {
                    { "Name", "DS" },
                };

            string json = JsonConvert.SerializeObject(values, Formatting.Indented);
            var content = new StringContent(json);
            var response = await _client.PostAsync("http://localhost:8081/people", content);

            var responseString = await response.Content.ReadAsStringAsync();

            var actual = _requestHandler.CreatePerson(context);
            var expected = "test";

            Assert.Equal(expected, actual);
            mockServer.Stop();
        }
    }
}
