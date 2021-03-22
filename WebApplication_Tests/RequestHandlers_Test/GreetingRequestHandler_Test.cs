using System;
using Xunit;
using System.Net;
using WebApplication.RequestHandlers;
using WebApplication;
using System.Net.Http;
using System.IO;
using Moq;
using System.Text;
using System.Collections.Generic;
using WebApplication.Repositories;

namespace WebApplication_Tests
{
    public class GreetingRequestHandler_Test
    {

        private readonly Repository _repository;
        private readonly GreetingRequestHandler _greetingRequestHandler;
        private HttpClient _client;
        public GreetingRequestHandler_Test()
        {
            _repository = new Repository();
            _greetingRequestHandler = new GreetingRequestHandler(_repository);
            _client = new HttpClient();
        }
        [Fact]
        public void HandleGreeting_ReturnCorrectGreetingMessage()
        {
            var request = Mock.Of<IRequest>(r => r.HttpMethod == "GET");
            var response = Mock.Of<IResponse>(r => r.OutputStream == new MemoryStream());
            var context = Mock.Of<IContext>(c => c.Request == request && c.Response == response);
            
            _greetingRequestHandler.HandleRequest(context);
            
            var time = DateTime.Now.ToString("HH:mm");
            var date = DateTime.Now.ToString("dd MMM yyyy");
            var expectedResponse = $"Hello Tiffany - the time on the server is {time} on {date}";
            
            Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
            
            response.OutputStream.Position = 0;
            
            var actualResponse = new StreamReader(response.OutputStream, Encoding.UTF8).ReadToEnd();
            Assert.Equal(expectedResponse, actualResponse);
        }
    }

}
