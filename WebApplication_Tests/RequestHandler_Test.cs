using System;
using Xunit;
using WebApplication;
namespace WebApplication_Tests
{
    public class RequestHandler_Test
    {
        private readonly Repository _repository;
        private readonly string responseString;
        public RequestHandler_Test()
        {
            _repository = new Repository();
        }

        [Fact]
        public void Greeting_ReturnCorrectGreetingMessage()
        {
            var requestHandler = new RequestHandler(_repository);
            var responseString = requestHandler.Greeting();
            var time = DateTime.Now.ToString("HH:mm");
            var date = DateTime.Now.ToString("dd MMM yyyy");
            var expected = $"Hello Tiffany - the time on the server is {time} on {date}";

            Assert.Equal(expected, responseString);
        }
    }
}
