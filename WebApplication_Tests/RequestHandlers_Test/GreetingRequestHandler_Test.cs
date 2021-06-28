using System;
using Xunit;
using System.Net;
using WebApplication.RequestHandlers;
using System.IO;
using Moq;
using System.Text;
using WebApplication.Http;
using WebApplication.Services;
using System.Collections.Specialized;

namespace WebApplication_Tests
{
    public class GreetingRequestHandler_Test
    {

        private Mock<IGreetingService> _greetingService;
        private readonly GreetingRequestHandler _greetingRequestHandler;
        private readonly NameValueCollection _headers;
        public GreetingRequestHandler_Test()
        {
            var secret = Environment.GetEnvironmentVariable("SECRET");
            _greetingService = new Mock<IGreetingService>();
            _greetingRequestHandler = new GreetingRequestHandler(_greetingService.Object);
            _headers = new NameValueCollection();
            _headers.Add("Authorization", "Basic " + secret);
        }
        [Fact]
        public void HandleGreeting_ReturnCorrectGreetingMessage()
        {
            var request = Mock.Of<IRequest>(r => r.HttpMethod == "GET" &&
                r.Headers == _headers);
            var response = Mock.Of<IResponse>(r => r.OutputStream == new MemoryStream());
            var context = Mock.Of<IContext>(c => c.Request == request && c.Response == response);

            var time = DateTime.Now.ToUniversalTime().ToString("HH:mm");
            var date = DateTime.Now.ToUniversalTime().ToString("dd MMM yyyy");
            var expectedResponse = $"Hello Tiffany - The time on the server is {time} on {date}";


            _greetingService.Setup(s => s.Greeting()).Returns(expectedResponse);

            _greetingRequestHandler.HandleRequest(context);

            Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);

            response.OutputStream.Position = 0;

            var actualResponse = new StreamReader(response.OutputStream, Encoding.UTF8).ReadToEnd();
            Assert.Equal(expectedResponse, actualResponse);
        }

        [Fact]
        public void InvalidMethod_RespondMethodNotAllowedStatus()
        {
            var request = Mock.Of<IRequest>(r =>
                r.HttpMethod == "INVALIDMETHOD" &&
                r.Headers == _headers);
            var response = Mock.Of<IResponse>(r => r.OutputStream == new MemoryStream());
            var context = Mock.Of<IContext>(c => c.Request == request && c.Response == response);

            _greetingRequestHandler.HandleRequest(context);

            Assert.Equal((int)HttpStatusCode.MethodNotAllowed, response.StatusCode);
        }

        [Fact]
        public void ValidateAuthentication_NoHeader_ReturnUnauthorizedStatus()
        {
            var request = Mock.Of<IRequest>(r => r.HttpMethod == "GET");
            var response = Mock.Of<IResponse>(r => r.OutputStream == new MemoryStream());
            var context = Mock.Of<IContext>(c => c.Request == request && c.Response == response);

            _greetingRequestHandler.HandleRequest(context);

            Assert.Equal((int)HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
